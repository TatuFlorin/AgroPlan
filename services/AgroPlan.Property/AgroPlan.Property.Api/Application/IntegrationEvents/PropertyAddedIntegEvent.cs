using AgroPlan.Common.ServiceBus.Events;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.IntegrationEvents
{
    public sealed class PropertyAddedIntegEvent : IntegrationEvent
    {

        public PropertyAddedIntegEvent(string ownerId, float surface)
        {
            OwnerId = ownerId;
            Surface = surface;
        }

        public string OwnerId { get; set; }
        public float Surface { get; set; }
    }
}