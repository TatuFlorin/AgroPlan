using MediatR;

namespace AgroPlan.Property.AgroPlan.Property.Core.DomainEvents
{
    public sealed class PropertyAddedDomainEvent : INotification
    {

        public PropertyAddedDomainEvent(string ownerId, float surface)
        {
            this.OwnerId = ownerId;
            this.Surface = surface;
        }

        public string OwnerId { get; set; }
        public float Surface { get; set; }
    }
}