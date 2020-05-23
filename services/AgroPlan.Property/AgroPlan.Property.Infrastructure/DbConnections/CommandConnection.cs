namespace AgroPlan.Property.AgroPlan.Property.Infrastructure.DbConnections{
    public class CommandConnection{
        public CommandConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; protected set; }
    }
}