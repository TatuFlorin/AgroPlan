namespace AgroPlan.Planification.Infrastructure.DbConnections
{
    public sealed class CommandConnectionString
    {
        public CommandConnectionString(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}