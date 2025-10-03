namespace MetricsApi.Web.Middewares
{
    public static class WebApplicationMiddlewareExtensions
    {
        public static WebApplication UseCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}
