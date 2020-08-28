using MediatR;

namespace AgroPlan.Property.AgroPlan.Property.Core.DomainEvents
{
    public sealed class PropertyRemovedDomainEvent : INotification
    {

        public PropertyRemovedDomainEvent(string ownerId, float surface)
        {
            OwnerId = ownerId;
            Surface = surface;
        }
        
        public string OwnerId { get; set; }
        public float Surface { get; set; }
    } 
}