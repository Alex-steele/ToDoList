using ToDoList.Utilities;

namespace ToDoList.Data
{
    internal class DataConfiguration : IDataConfiguration
    {
        // Not used anymore
        public DataConfiguration(string connectionString)
        {
            Check.NotNullOrWhiteSpace(connectionString, nameof(connectionString));
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
