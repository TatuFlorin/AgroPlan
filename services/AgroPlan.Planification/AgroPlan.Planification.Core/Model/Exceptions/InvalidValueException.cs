namespace AgroPlan.Planification.Core.Model.Exceptions
{
    public class InvalidValueException : System.Exception
    {
        public InvalidValueException() { }
        public InvalidValueException(string message) : base(message) { }
        public InvalidValueException(string message, System.Exception inner) : base(message, inner) { }
    }
}