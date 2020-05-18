namespace AgroPlan.Planification.Infrastructure.DbConnections
{
    public class QueryConnectionString
    {
        public string Value { get; private set; }

        public QueryConnectionString(string value)
        {
            Value = value;
        }

        private QueryConnectionString() { }
    }
}