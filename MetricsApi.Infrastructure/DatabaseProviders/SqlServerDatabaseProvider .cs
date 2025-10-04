using MetricsApi.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Infrastructure.DatabaseProviders
{
    public class SqlServerDatabaseProvider : IDatabaseProvider
    {
        private readonly string _connectionString;

        public SqlServerDatabaseProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString() => _connectionString;
        public string GetProviderName() => "SqlServer";
    }
}
