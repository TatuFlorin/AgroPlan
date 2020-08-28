using System.Collections.Generic;
using MediatR;

namespace AgroPlan.Property.AgroPlan.Property.Core{
    public abstract class Entity<T> 
    {
        public T Id {get; protected set;}

        private List<INotification> _eventList;
        public IReadOnlyList<INotification> EventList => _eventList?.AsReadOnly();

        protected void AddEvent(INotification @event)
        {
            _eventList = _eventList ?? new List<INotification>();
            _eventList.Add(@event);
        }

        protected void RemoveEvent(INotification @event)
        {
            _eventList?.Remove(@event);
        }

        public void ClearEvents()
            => _eventList?.Clear();

        public Entity(T id)
        { this.Id = id; }

        public Entity() {}

        //override        
    }
}