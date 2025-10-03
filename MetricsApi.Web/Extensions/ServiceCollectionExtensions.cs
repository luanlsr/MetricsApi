using FluentValidation;
using MediatR;
using MetricsApi.Domain.Abstractions;
using MetricsApi.Domain.Common;
using MetricsApi.Domain.Repositories;
using MetricsApi.Infrastructure.Context;
using MetricsApi.Infrastructure.DatabaseProviders;
using MetricsApi.Infrastructure.Events;
using MetricsApi.Infrastructure.Persistence;
using MetricsApi.Infrastructure.Repositories;
using MetricsApi.Web.Settings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MetricsApi.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDependencies(
            this IServiceCollection services,
            DatabaseSettings databaseSettings)
        {
            // 1️⃣ MediatR - registra todos os handlers da camada Application
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(Assembly.Load("MetricsApi.Application"))
            );

            // 2️⃣ FluentValidation (novo padrão .NET 8)
            services.AddValidatorsFromAssembly(Assembly.Load("MetricsApi.Application"));

            // 3️⃣ Controllers
            services.AddControllers();

            // 4️⃣ Database provider dinâmico
            IDatabaseProvider databaseProvider = databaseSettings.Provider switch
            {
                "PostgreSQL" => new PostgreSqlDatabaseProvider(databaseSettings.ConnectionString),
                "SqlServer" => new SqlServerDatabaseProvider(databaseSettings.ConnectionString),
                _ => throw new InvalidOperationException($"Database provider '{databaseSettings.Provider}' not supported")
            };
            services.AddSingleton(databaseProvider);

            // 5️⃣ DbContext configurado dinamicamente
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var provider = sp.GetRequiredService<IDatabaseProvider>();

                switch (provider.GetProviderName())
                {
                    case "PostgreSQL":
                        options.UseNpgsql(provider.GetConnectionString());
                        break;
                    case "SqlServer":
                        options.UseSqlServer(provider.GetConnectionString());
                        break;
                    default:
                        throw new InvalidOperationException($"Database provider '{provider.GetProviderName()}' not supported");
                }
            });

            // 6️⃣ UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 6.1️⃣ EventPublisher
            services.AddSingleton<IEventPublisher, ConsoleEventPublisher>();

            // 7️⃣ Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
