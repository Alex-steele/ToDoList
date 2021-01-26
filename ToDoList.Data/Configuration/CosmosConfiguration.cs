using ToDoList.Utilities;

namespace ToDoList.Data.Configuration
{
    internal class CosmosConfiguration : ICosmosConfiguration
    {
        public CosmosConfiguration(string connectionString)
        {
            Check.NotNullOrWhiteSpace(connectionString, nameof(connectionString));
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
