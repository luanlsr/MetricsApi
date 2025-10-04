using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MetricsApi.Infrastructure.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=metrics;User Id=sa;Password=SuaSenhaForte!2025;TrustServerCertificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
