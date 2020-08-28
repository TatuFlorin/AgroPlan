using AgroPlan.Common.ServiceBus.Events;
using AgroPlan.Common.ServiceBus.Interfaces;
using System.Threading.Tasks;

namespace AgroPlan.Common.ServiceBus.Interfaces
{
    public interface IServiceBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler;
        void SubscribeDynamic<EH>(string eventName)
            where EH : IDynamicIntegrationEventHandler;

        void Unsubscribe<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler;
        void UnsubscribeDynamic<EH>(string eventName)
            where EH : IDynamicIntegrationEventHandler;
    }
}