using System;
using System.Threading.Tasks;
using AgroPlan.Common.ServiceBus.Events;
using AgroPlan.Common.ServiceBus.Interfaces;
using AgroPlan.Property.AgroPlan.Property.Infrastructure;
using Microsoft.Extensions.Logging;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.IntegrationEvents
{
    public sealed class IntegrationEventService : IIntegrationEventService
    {

        private readonly IServiceBus _bus;
        private readonly ILogger<IntegrationEventService> _logger;
        private readonly PropertyContext _context;

        public IntegrationEventService(IServiceBus bus
            , ILogger<IntegrationEventService> logger
            , PropertyContext context)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(IntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(IntegrationEventService));
            _context = context ?? throw new ArgumentNullException(nameof(IntegrationEventService));
        }

        public Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _bus.Publish(evt);

            return Task.CompletedTask;
        }

        public Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}