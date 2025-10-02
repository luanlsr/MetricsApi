namespace MetricsApi.Web.Extensions
{
    public static class CorsExtensions
    {
        private const string CorsPolicyName = "DefaultCorsPolicy";

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return services;
        }

        public static WebApplication UseCorsPolicy(this WebApplication app)
        {
            app.UseCors(CorsPolicyName);
            return app;
        }
    }
}
