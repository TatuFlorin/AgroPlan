namespace AgroPlan.Property.AgroPlan.Property.Core.Exceptions{

    public class TakenParcelException : System.Exception
    {
        public TakenParcelException() { }
        public TakenParcelException(string message) : base(message) { }
        public TakenParcelException(string message, System.Exception inner) : base(message, inner) { }
    }
}