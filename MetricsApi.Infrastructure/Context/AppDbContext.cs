using Microsoft.EntityFrameworkCore;

namespace MetricsApi.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Product> Products { get; set; }
        // Adicionar outros DbSets conforme necessário
    }
}
