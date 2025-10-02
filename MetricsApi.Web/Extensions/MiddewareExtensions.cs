namespace MetricsApi.Web.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseCustomMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
