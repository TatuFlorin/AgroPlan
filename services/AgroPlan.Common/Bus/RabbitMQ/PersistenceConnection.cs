using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Polly.Retry;
using System.Net.Sockets;
using RabbitMQ.Client.Exceptions;
using Polly;
using RabbitMQ.Client.Events;

namespace AgroPlan.Common.RabbitMQ
{
    public class PresistenceConnection : IPersistenceConnection
    {

        private readonly IConnectionFactory _connectionFactory;
        private ILogger<PresistenceConnection> _logger;
        private int _tryCount;
        IConnection _connection;
        bool _dispose;

        object sync_root = new object();

        public PresistenceConnection(
            IConnectionFactory connectionFactory
            , ILogger<PresistenceConnection> logger
            , int tryCount = 3
        )
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(PresistenceConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(PresistenceConnection));
            _tryCount = tryCount;
        }


        public bool IsConnected 
            => _connection != null && _connection.IsOpen && !_dispose;

        public IModel CreateModel()
        {
            if(!IsConnected)
                throw new InvalidOperationException(
                    "No RabbitMQ connections are available to perform this action"
                );

            return _connection.CreateModel();
        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabitMQ is trying to connect!");

            lock(sync_root)
            {
                var policy = RetryPolicy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(
                        _tryCount,
                        retryAttempts => TimeSpan.FromSeconds(Math.Pow(2, retryAttempts)),
                        (ex, time) => 
                        {
                            _logger.LogWarning(
                                ex, 
                                "RabbitMQ couldn't connect after { TimeOut }s ({ ExceptionMessage })",
                                $"{time.TotalSeconds:n1}",
                                ex.Message
                            );
                        }
                    );

                policy.Execute( () => {
                    _connection = _connectionFactory.CreateConnection();
                });

                if(IsConnected)
                {
                    _connection.ConnectionBlocked += OnConnectionBlocked;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionShutdown += OnConnectionShutdown;

                    _logger.LogInformation("");

                    return true;
                }
                else
                {
                    _logger.LogCritical("");

                    return false;
                }
            }

        }

        public void Dispose()
        {
            if(!_dispose)
                return;
            
            _dispose = true;

            try
            {
                _connection.Dispose();
            }
            catch(IOException ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }
    
    
        #region Events
            private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
            {
                if(_dispose) return;

                _logger.LogWarning("");

                TryConnect();
            }

            private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
            {
                if(_dispose) return;

                _logger.LogWarning("");

                TryConnect();
            }

            private void OnConnectionShutdown(object sender, ShutdownEventArgs e)
            {
                if(_dispose) return;

                _logger.LogWarning("");

                TryConnect();
            }
        #endregion
    }
}