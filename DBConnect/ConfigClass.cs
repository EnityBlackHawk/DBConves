using DBTelegraph.Model;

namespace DBTelegraph
{
    public class ConfigClass
    {
        public string ConnectionString { get; } = "Server=BLACKHAWKPC\\SQLSERVER;Trusted_Connection=True;";
        public SGBD SGBD { get; } = SGBD.SQL_SERVER;
        public string ProviderName { get; } = "System.Data.SqlClient";
        
        public string? DatabaseName { get; } 

        public string GetConnectionStringForDataBase() => ConnectionString + $"Database={DatabaseName};";
        public string GetConnectionStringForDataBase(string databaseName) => ConnectionString + $"Database={databaseName};";



        public ConfigClass(string connectionString, SGBD sgdb, string? databaseName)
        {
            ConnectionString = connectionString;
            SGBD = sgdb;
            DatabaseName = databaseName;
        }
    }
}