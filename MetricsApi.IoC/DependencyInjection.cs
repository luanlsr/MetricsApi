using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MetricsApi.CrossCutting.Settings;
using MetricsApi.Domain.Abstractions;
using MetricsApi.Domain.Common;
using MetricsApi.Domain.Repositories;
using MetricsApi.Infrastructure.Context;
using MetricsApi.Infrastructure.DatabaseProviders;
using MetricsApi.Infrastructure.Events;
using MetricsApi.Infrastructure.Persistence;
using MetricsApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MetricsApi.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            DatabaseSettings databaseSettings)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(Assembly.Load("MetricsApi.Application"))
            );

            services.AddValidatorsFromAssembly(Assembly.Load("MetricsApi.Application"));

            services.AddControllers();

            IDatabaseProvider databaseProvider = databaseSettings.Provider switch
            {
                "SqlServer" => new SqlServerDatabaseProvider(databaseSettings.ConnectionString),
                _ => throw new InvalidOperationException($"Database provider '{databaseSettings.Provider}' not supported")
            };
            services.AddSingleton(databaseProvider);

            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var provider = sp.GetRequiredService<IDatabaseProvider>();

                switch (provider.GetProviderName())
                {
                    case "SqlServer":
                        options.UseSqlServer(provider.GetConnectionString());
                        break;
                    default:
                        throw new InvalidOperationException($"Database provider '{provider.GetProviderName()}' not supported");
                }
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IEventPublisher, ConsoleEventPublisher>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
