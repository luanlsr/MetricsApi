namespace MetricsApi.Web.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseCustomMiddlewares(this WebApplication app)
        {
            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            // Middleware de exceções
            app.UseExceptionMiddleware();

            // HTTPS
            app.UseHttpsRedirection();

            // Autenticação e autorização
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
