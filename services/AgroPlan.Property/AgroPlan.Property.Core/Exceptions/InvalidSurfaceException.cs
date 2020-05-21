namespace AgroPlan.Property.AgroPlan.Core.Exceptions{
    public class InvalidSurfaceException : System.Exception
    {
        public InvalidSurfaceException() { }
        public InvalidSurfaceException(string message) : base(message) { }
        public InvalidSurfaceException(string message, System.Exception inner) : base(message, inner) { }
    }
}
