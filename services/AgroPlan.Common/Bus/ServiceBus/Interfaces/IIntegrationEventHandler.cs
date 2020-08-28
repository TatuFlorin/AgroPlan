using AgroPlan.Common.ServiceBus.Events;
using System.Threading.Tasks;

namespace AgroPlan.Common.ServiceBus.Interfaces
{
    public interface IIntegrationEventHandler<in TIntegretionEvent> : IIntegrationEventHandler
        where TIntegretionEvent : IntegrationEvent
    {
        Task Handler(TIntegretionEvent @event);
    }

    public interface IIntegrationEventHandler
    {

    }
}