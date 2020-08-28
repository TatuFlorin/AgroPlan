namespace AgroPlan.Common.ServiceBus.Exceptions
{
    [System.Serializable]
    public class BrokerUnreachableExceptionException : System.Exception
    {
        public BrokerUnreachableExceptionException() { }
        public BrokerUnreachableExceptionException(string message) : base(message) { }
        public BrokerUnreachableExceptionException(string message, System.Exception inner) : base(message, inner) { }
        protected BrokerUnreachableExceptionException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}