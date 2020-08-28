using System;
using System.Linq;
using System.Collections.Generic;
using AgroPlan.Common.ServiceBus.Events;
using AgroPlan.Common.ServiceBus.Interfaces;

namespace AgroPlan.Common.ServiceBus
{
    ///This is InMemory subscripion manager

    public class SubscriptionManager : ISubscriptionManager
    {
        
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public SubscriptionManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }
        
        public bool isEmpty => !_handlers.Keys.Any();
        public void Clear() => _handlers.Clear();

        #region Private methods
            private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
            {
                if(!HasSubscriptionForEvent(eventName))
                    _handlers.Add(eventName, new List<SubscriptionInfo>());

                if(_handlers[eventName].Any(x => x.HandlerType == handlerType))
                    throw new ArgumentException(
                        $"Handler Type { handlerType } already registered for '{ eventName }'",
                        nameof(handlerType)
                        );

                if(isDynamic)
                    _handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
                else 
                    _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            }

            private SubscriptionInfo FindSubsctiption(string eventName, Type handlertype)
            {
                if(!HasSubscriptionForEvent(eventName))
                    return null;

                return _handlers[eventName].Single(x => x.HandlerType == handlertype);
            }

            private SubscriptionInfo FindToRemoveSubscription<E, EH>()
                where E : IntegrationEvent
                where EH : IIntegrationEventHandler
            {
                var eventName = GetEventKey<E>();

                return FindSubsctiption(eventName, typeof(EH));
            }

            private SubscriptionInfo FindToRemoveDynamicSubsctiption<EH>(string eventName)
                where EH : IDynamicIntegrationEventHandler
            {
                return FindSubsctiption(eventName, typeof(EH));
            } 

            private void  DoRemoveHandler(string eventName, SubscriptionInfo info)
            {
                if(!(info is null))
                {
                    _handlers[eventName].Remove(info);
                    if(!_handlers[eventName].Any())
                    {
                        _handlers.Remove(eventName);
                        var eventType = _eventTypes.FirstOrDefault(x => x.Name == eventName);

                        if(eventName != null)
                            _eventTypes.Remove(eventType);

                        RaiseOnEventRemoved(eventName);
                    }
                }
            }
        
        #endregion

        #region Events
            private void RaiseOnEventRemoved(string eventName)
            {
                var handler = OnEventRemoved;
                handler?.Invoke(this, eventName);
            }
        #endregion

        public void AddDynamicSubscription<EH>(string eventName) 
            where EH : IDynamicIntegrationEventHandler
        {
            DoAddSubscription(typeof(EH), eventName, true);
        }

        public void AddSubscription<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler
        {
            var key = GetEventKey<E>();
            DoAddSubscription(typeof(EH), key, false);

            if(!_eventTypes.Contains(typeof(E)))
                _eventTypes.Add(typeof(E));
        }


        public void RemoveDynamicSubscritpion<EH>(string eventName) 
            where EH : IDynamicIntegrationEventHandler
        {
            var sub = FindToRemoveDynamicSubsctiption<EH>(eventName);
            DoRemoveHandler(eventName, sub);
        }

        public void RemoveSubscritpion<E, EH>()
            where E : IntegrationEvent
            where EH : IIntegrationEventHandler
        {
            var sub = FindToRemoveSubscription<E, EH>();
            var key = GetEventKey<E>();
            DoRemoveHandler(key, sub);
        }

        public string GetEventKey<T>()
            => typeof(T).Name;

        public Type GetEventTypeByName(string eventName)
        {
            return _eventTypes.SingleOrDefault(x => x.Name == eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<E>() 
            where E : IntegrationEvent
        {
            var key = GetEventKey<E>();
            return GetHandlersForEvent(key);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            return _handlers[eventName];
        }

        public bool HasSubscriptionForEvent<E>() 
            where E : IntegrationEvent
        {
            var key = GetEventKey<E>();
            return HasSubscriptionForEvent(key);
        }

        public bool HasSubscriptionForEvent(string eventName)
        {
            return _handlers.ContainsKey(eventName);
        }

    }
}