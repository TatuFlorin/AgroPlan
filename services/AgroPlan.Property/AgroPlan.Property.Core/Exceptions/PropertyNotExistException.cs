namespace AgroPlan.Property.AgroPlan.Property.Core.Exceptions{
    [System.Serializable]
    public class PropertyNotExistException : System.Exception
    {
        public PropertyNotExistException() { }
        public PropertyNotExistException(string message) : base(message) { }
        public PropertyNotExistException(string message, System.Exception inner) : base(message, inner) { }
    }
}