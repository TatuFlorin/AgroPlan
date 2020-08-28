using System.Threading.Tasks;
using AgroPlan.Common.ServiceBus;
using AgroPlan.Common.ServiceBus.Events;
using AgroPlan.Common.ServiceBus.Interfaces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Autofac;
using Microsoft.Extensions.Logging;
using System.Text;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;

namespace AgroPlan.Common.RabbitMQ
{
    public class RabbitMQ : IServiceBus, IDisposable
    {
        const string BROKER_NAME = "agroplan_event_bus";

        private readonly string AUTOFAC_SCOPE_NAME = "agroplan_event_bus";
        private readonly IPersistenceConnection _persConnection;
        private readonly ISubscriptionManager _subManager;
        private readonly ILogger<RabbitMQ> _logger;
        private readonly IServiceCollection _scope;
        private readonly int _retryCount;
        private IModel _consumerChannel;
        private string _queueName;        

        public RabbitMQ(
            ISubscriptionManager subManager
            , IPersistenceConnection persConnection
            , IServiceCollection scope
            , ILogger<RabbitMQ> logger
            , string queueName = null
            , int retryCount = 3
        )
        {
            _subManager = subManager;
            _persConnection = persConnection;
            _logger = logger;
            _scope = scope;
            _queueName = queueName;
            _retryCount = retryCount;
            _consumerChannel = CreateConsumerChannel();
            _subManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        #region Private methods
            private void SubsManager_OnEventRemoved(object sender, string eventName)
            {
                if(!_persConnection.IsConnected)
                    _persConnection.TryConnect();

                using(var channel = _persConnection.CreateModel())
                {
                    channel.QueueUnbind
                    (
                        queue: _queueName,
                        exchange: BROKER_NAME,
                        routingKey: eventName
                    );

                    if(_subManager.isEmpty)
                    {
                        _queueName = string.Empty;
                        _consumerChannel.Close();
                    }
                }
            }
        
            private IModel CreateConsumerChannel()
            {
                if(!_persConnection.IsConnected)
                    _persConnection.TryConnect();

                _logger.LogInformation("Creating RabbitMQ consumer channel...");

                var channel = _persConnection.CreateModel();

                channel.ExchangeDeclare(
                    exchange: BROKER_NAME,
                    type: "direct"
                );

                channel.QueueDeclare(
                    queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                channel.CallbackException += (sender, ea) => 
                {
                    _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel...");

                    _consumerChannel.Dispose();
                    _consumerChannel = CreateConsumerChannel();
                    StartBasicConsume();
                };

                return channel;

            }

            private void StartBasicConsume()
            {
                _logger.LogTrace("Starting RabbitMQ basic consume..");

                if(_consumerChannel != null)
                {
                    var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                    consumer.Received += Consumer_Reseved;

                    _consumerChannel.BasicConsume
                    (
                        queue: _queueName,
                        autoAck: false,
                        consumer: consumer
                    );
                }
                else
                {
                    _logger.LogError("StartBasicConsume -> _consumerChannel is null");
                }
            }
        
            private async Task Consumer_Reseved(object sender, BasicDeliverEventArgs eventArgs)
            {
                var eventName = eventArgs.RoutingKey;
                var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                try
                {
                    if(message.ToLowerInvariant().Contains("throw_fake_exception"))
                    {
                        throw new InvalidOperationException($"Fake exception required: \"{ message }\"");
                    }

                    await ProcessEvent(eventName, message);
                }
                catch(Exception ex)
                {
                    _logger.LogWarning(ex, $"......... ERROR Processing message \" { message } \"");
                }

                _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
        
            private async Task ProcessEvent(string eventName, string message)
            {
                _logger.LogTrace("Processing RabbitMQ event: {eventName}", eventName);

                if(_subManager.HasSubscriptionForEvent(eventName))
                {
                    // using(var scope = _scope.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                    // {
                        var subscribers = _subManager.GetHandlersForEvent(eventName);

                        foreach(var subscriber in subscribers)
                        {
                            if(subscriber.IsDynamic)
                            {
                                var handler = _scope.AddScoped(subscriber.HandlerType) as IDynamicIntegrationEventHandler;
                                if(handler is null) continue;
                                dynamic eventData = JObject.Parse(message);

                                await Task.Yield();
                                await handler.Handler(eventData);
                            }
                            else
                            {
                                var handler = _scope.AddScoped(subscriber.HandlerType);
                                if(handler is null) continue;
                                var eventType = _subManager.GetEventTypeByName(eventName);
                                var eventData = JsonConvert.DeserializeObject(message, eventType);
                                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                                await Task.Yield();
                                await (Task)concreteType.GetMethod("Handler")
                                    ?.Invoke(handler, new object[] { eventData });

                            }
                        }
                    // }
                }
                else
                {
                    _logger.LogWarning("No subscribers for RabbitMQ event: \"{EventName}\"", eventName);
                }
            }
       
            private void DoInternalSubscription(string eventName)
            {
                var containsKey = _subManager.HasSubscriptionForEvent(eventName);

                if(!_persConnection.IsConnected)
                    _persConnection.TryConnect();

                using(var channel = _persConnection.CreateModel())
                {
                    channel.QueueBind(
                        queue: _queueName,
                        exchange: BROKER_NAME,
                        routingKey: eventName
                    );
                }
            }
        #endregion

        public void Publish(IntegrationEvent @event)
        {
            if(!_persConnection.IsConnected)
                _persConnection.TryConnect();

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                            .Or<SocketException>()
                            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                            , (ex, time) => 
                            {
                                _logger.LogWarning
                                (
                                    ex,
                                    "Could not publish event: {EventId} after {Timeout} ({ExceptionMessage}))"
                                    , @event.EventId
                                    , $"{time.TotalSeconds:n1}"
                                    , ex.Message
                                );
                            });

            var eventName = @event.GetType().Name;
            
            _logger.LogTrace("Creating channel to publish event: {EventId} ({EventName})", @event.EventId, eventName);

            using(var channel = _persConnection.CreateModel())
            {
                _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.EventId);

                channel.ExchangeDeclare
                (
                    exchange: BROKER_NAME,
                    type: "direct"
                );

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() => 
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;
                    
                    _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.EventId);

                    channel.BasicPublish
                    (
                      exchange: BROKER_NAME,
                      routingKey: eventName,
                      mandatory: true,
                      basicProperties: properties,
                      body: body
                    );
                });
            }
        }

        public void SubscribeDynamic<EH>(string eventName)
            where EH : IDynamicIntegrationEventHandler
        {
            DoInternalSubscription(eventName);

            _logger.LogInformation(
                "Subscribing to dynamic event {EventName} with {EventHandler}"
                , eventName
                , typeof(EH).Name
                );

            _subManager.AddDynamicSubscription<EH>(eventName);
            StartBasicConsume();
        }

        public void Subscribe<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler
        {
            var eventName = _subManager.GetEventKey<E>();
            DoInternalSubscription(eventName);

            _logger.LogInformation(
                "Subscribing to event: {EventName} with handler: {EventHandler}"
                , eventName
                , typeof(EH).Name
                );

            _subManager.AddSubscription<E, EH>();
            StartBasicConsume();
        }

        public void Unsubscribe<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler
        {
            var eventName = _subManager.GetEventKey<E>();

            _logger.LogInformation("Unsubscribe form event: {EventName}", eventName);

            _subManager.RemoveSubscritpion<E, EH>();
        }

        public void UnsubscribeDynamic<EH>(string eventName) 
            where EH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("Unsubscribe form dynamic event: {EventName}", eventName);

            _subManager.RemoveDynamicSubscritpion<EH>(eventName);
        }

        public void Dispose()
        {
            if(_consumerChannel != null)
                _consumerChannel.Dispose();

            _subManager.Clear();
        }
    }
}