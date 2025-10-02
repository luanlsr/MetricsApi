using Microsoft.Extensions.DependencyInjection;

namespace MetricsApi.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // Repositórios
            // Exemplo: IUserRepository -> UserRepository
            // services.AddScoped<IUserRepository, UserRepository>();

            // Serviços de domínio/infrastructure
            // services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
