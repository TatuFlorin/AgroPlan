namespace AgroPlan.Property.AgroPlan.Core.Exceptions{

    public class BusyParcelException : System.Exception
    {
        public BusyParcelException() { }
        public BusyParcelException(string message) : base(message) { }
        public BusyParcelException(string message, System.Exception inner) : base(message, inner) { }
    }
}