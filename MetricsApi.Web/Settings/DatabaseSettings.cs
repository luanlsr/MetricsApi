namespace MetricsApi.Web.Settings
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string Provider { get; set; } = "PostgreSQL"; // opcional: PostgreSQL, SQLServer, etc.
    }
}
