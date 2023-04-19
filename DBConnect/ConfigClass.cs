namespace DBTelegraph
{
    public static class ConfigClass
    {
        public static string ConnectionString { get; } = "Server=BLACKHAWKPC\\SQLSERVER;Trusted_Connection=True;";

        public static string ProviderName { get; } = "System.Data.SqlClient";
        public static string GetConnectionStringForDataBase(string dataBaseName) => ConnectionString + $"Database={dataBaseName};";
    }
}