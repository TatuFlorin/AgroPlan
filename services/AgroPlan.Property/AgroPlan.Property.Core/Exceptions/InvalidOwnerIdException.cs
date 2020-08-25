namespace AgroPlan.Property.AgroPlan.Property.Core.Exceptions{
    public class InvalidOwnerIdException : System.Exception
    {
        public InvalidOwnerIdException() { }
        public InvalidOwnerIdException(string message) : base(message) { }
        public InvalidOwnerIdException(string message, System.Exception inner) : base(message, inner) { }
    }
}