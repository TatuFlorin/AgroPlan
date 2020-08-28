using AgroPlan.Property.AgroPlan.Property.Api.Application.IntegrationEvents;
using AgroPlan.Property.AgroPlan.Property.Core.DomainEvents;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using System;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.DomainEventsHandlers
{
    public sealed class PropertyAddedDomainEventHandler
        : INotificationHandler<PropertyAddedDomainEvent>
    {

        private readonly ILogger<PropertyAddedDomainEventHandler> _logger;
        private readonly IIntegrationEventService _integService;
        private readonly IMediator _mediator;

        public PropertyAddedDomainEventHandler(
            ILogger<PropertyAddedDomainEventHandler> logger
            , IMediator mediator
            , IIntegrationEventService integService
        )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(PropertyAddedDomainEventHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(PropertyAddedDomainEventHandler));
            _integService = integService ?? throw new ArgumentNullException(nameof(PropertyAddedDomainEventHandler));
        }

        public Task Handle(PropertyAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            _ = notification 
                ?? throw new ArgumentNullException(nameof(PropertyAddedDomainEventHandler));

            _logger.LogInformation(
                "An new property was added for {OwenrId} with a surface about {Surface}"
                , notification.OwnerId
                , notification.Surface
                );


            // Trigger integration event
            var propertyAddedIntegEvent = new PropertyAddedIntegEvent(notification.OwnerId, notification.Surface);

            _integService.AddAndSaveEventAsync(propertyAddedIntegEvent);

            return Task.CompletedTask;
        }
    }
}