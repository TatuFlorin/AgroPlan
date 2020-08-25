namespace AgroPlan.Property.AgroPlan.Property.Core.Exceptions{
    public class InvalidCodeException : System.Exception
    {
        public InvalidCodeException() { }
        public InvalidCodeException(string message) : base(message) { }
        public InvalidCodeException(string message, System.Exception inner) : base(message, inner) { }
    }
}