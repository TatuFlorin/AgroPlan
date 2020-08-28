using System;
using System.Threading.Tasks;
using AgroPlan.Common.ServiceBus.Events;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}