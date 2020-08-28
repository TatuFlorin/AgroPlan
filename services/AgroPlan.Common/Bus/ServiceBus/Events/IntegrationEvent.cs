using System;

namespace AgroPlan.Common.ServiceBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            this.EventId = Guid.NewGuid();
            this.Date = DateTime.Now;
        }

        public Guid EventId {get;set;}
        public DateTime Date {get;set;}
    }
}