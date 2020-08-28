using AgroPlan.Common.ServiceBus.Events;
using AgroPlan.Common.ServiceBus.Interfaces;
using System;
using System.Collections.Generic;

namespace AgroPlan.Common.ServiceBus
{
    public interface ISubscriptionManager
    {
        bool isEmpty {get;}

        event EventHandler<string> OnEventRemoved;

        void AddDynamicSubscription<EH>(string eventName)
            where EH : IDynamicIntegrationEventHandler;

        void AddSubscription<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler;

        void RemoveDynamicSubscritpion<EH>(string eventName)
            where EH : IDynamicIntegrationEventHandler;

        void RemoveSubscritpion<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler;

        bool HasSubscriptionForEvent<E>()
            where E : IntegrationEvent;
        
        bool HasSubscriptionForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<E>()
            where E : IntegrationEvent;

        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<T>();
    }
}