namespace MetricsApi.CrossCutting.Settings
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string Provider { get; set; } = string.Empty;
    }
}
