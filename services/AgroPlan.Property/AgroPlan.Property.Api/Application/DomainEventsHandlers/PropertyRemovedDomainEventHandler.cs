using System;
using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Core.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.DomainEventsHandlers
{
    public sealed class PropertyRemovedDomainEventHandler
        : INotificationHandler<PropertyRemovedDomainEvent>
    {

        private readonly ILogger<PropertyRemovedDomainEventHandler> _logger;
        private readonly IMediator _mediator;
        public PropertyRemovedDomainEventHandler(
            ILogger<PropertyRemovedDomainEventHandler> logger
            , IMediator mediator
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(PropertyRemovedDomainEventHandler));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(PropertyRemovedDomainEventHandler));
        }

        public Task Handle(PropertyRemovedDomainEvent notification, CancellationToken cancellationToken)
        {

            _ = notification 
                ?? throw new ArgumentNullException(nameof(PropertyRemovedDomainEventHandler));

            _logger.LogInformation(
                "A property about {Surface} Ha was removed from owner {OwnerId}"
                , notification.OwnerId
                , notification.Surface
                );

            // Trigger integratin event
            // _mediator.Publish(
                
            // );

            return Task.CompletedTask;
        }
    }
}