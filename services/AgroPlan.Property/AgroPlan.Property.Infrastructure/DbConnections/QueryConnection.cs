namespace AgroPlan.Property.AgroPlan.Property.Infrastructure.DbConnections
{
    public sealed class QueryConnection
    {
        public QueryConnection(string connectionString)
        {
            this.Value = connectionString;
        }
        
        public string Value { get; private set;}
    }
}