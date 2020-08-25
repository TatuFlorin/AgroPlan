namespace AgroPlan.Property.AgroPlan.Property.Core.Exceptions
{
    [System.Serializable]
    public class OwnerNotFoundException : System.Exception
    {
        public OwnerNotFoundException() { }
        public OwnerNotFoundException(string message) : base(message) { }
        public OwnerNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected OwnerNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}