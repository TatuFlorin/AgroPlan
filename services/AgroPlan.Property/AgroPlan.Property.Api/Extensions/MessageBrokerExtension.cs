using AgroPlan.Common.RabbitMQ;
using AgroPlan.Common.ServiceBus;
using AgroPlan.Common.ServiceBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using sb = AgroPlan.Common.RabbitMQ;
using Autofac;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Extensions
{
    public static class MessageBrokerExtension
    {
        public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration configuration)
        {

            var subscriptionClientName = configuration.GetSection("RabbitMQ")["SubscriptionClientName"];
            var numberOfTry = int.Parse(configuration.GetSection("RabbitMQ")["TryCount"]);

            services.AddSingleton<ISubscriptionManager, SubscriptionManager>();

            services.AddSingleton<IPersistenceConnection>(sp => {
                var logger = sp.GetRequiredService<ILogger<PresistenceConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration.GetSection("RabbitMQ")["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                _ = !string.IsNullOrEmpty(configuration.GetSection("RabbitMQ")["UserName"]) 
                    ? factory.UserName = configuration.GetSection("RabbitMQ")["UserName"] : default;
                _ = !string.IsNullOrEmpty(configuration.GetSection("RabbitMQ")["Password"]) 
                    ? factory.Password = configuration.GetSection("RabbitMQ")["Password"] : default;
                _ = !string.IsNullOrEmpty(configuration.GetSection("RabbitMQ")["TryCount"]) 
                    ? numberOfTry = int.Parse(configuration.GetSection("RabbitMQ")["TryCount"]) : default;

                return new PresistenceConnection(
                    factory,
                    logger,
                    numberOfTry
                );
            });

            services.AddSingleton<IServiceBus, sb.RabbitMQ>(sp => {
                var persConnection = sp.GetRequiredService<IPersistenceConnection>();
                var logger = sp.GetRequiredService<ILogger<sb.RabbitMQ>>();
                var subManager = sp.GetRequiredService<ISubscriptionManager>();

                return new sb.RabbitMQ(
                    subManager,
                    persConnection,
                    services,
                    logger,
                    subscriptionClientName,
                    numberOfTry
                );
            });

            return services;
        }
    }
}