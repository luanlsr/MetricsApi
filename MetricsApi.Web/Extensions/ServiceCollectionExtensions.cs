using MetricsApi.Infrastructure.Context;
using MetricsApi.IoC;
using MetricsApi.Web.Settings;
using Microsoft.EntityFrameworkCore;

namespace MetricsApi.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurações
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));

            // Chamando IoC centralizado (Application + Repositórios + Serviços de infra)
            services.AddApplicationServices();

            // Configura EF Core com PostgreSQL
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Serviços API
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
