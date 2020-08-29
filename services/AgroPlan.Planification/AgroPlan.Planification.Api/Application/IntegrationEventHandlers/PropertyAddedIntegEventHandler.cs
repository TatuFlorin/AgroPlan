using System.Threading.Tasks;
using AgroPlan.Common.ServiceBus.Interfaces;
using AgroPlan.Planification.Api.Application.IntegrationEvents;
using AgroPlan.Planification.Infrastructure;
using Microsoft.Extensions.Logging;

namespace AgroPlan.Planification.Api.Application.IntegrationEventHandlers
{
    public sealed class PropertyAddedIntegEventHandler
        : IIntegrationEventHandler<PropertyAddedIntegEvent>
    {

        private readonly ILogger<PropertyAddedIntegEventHandler> _logger;
        private readonly PlanContext _context;

        public PropertyAddedIntegEventHandler(
            ILogger<PropertyAddedIntegEventHandler> logger
            ,  PlanContext context
        )
        {
            _logger = logger;
            _context = context;
        }

        public Task Handler(PropertyAddedIntegEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}