using System.Threading.Tasks;

namespace AgroPlan.Common.ServiceBus.Interfaces
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handler(dynamic eventData);
    }
}