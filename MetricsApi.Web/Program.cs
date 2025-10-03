using MetricsApi.Web.Extensions;
using MetricsApi.Web.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services
var databaseSettings = new DatabaseSettings
{
    ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!,
    Provider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "PostgreSQL"
};
builder.Services.AddApplicationDependencies(databaseSettings)
                .AddSwaggerDocumentation()
                .AddCorsPolicy()
                .AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Middlewares
app.UseSwaggerDocumentation()
   .UseCorsPolicy()
   .UseCustomMiddlewares()
   .UseHttpsRedirection()
   .UseAuthentication()
   .UseAuthorization();

app.MapControllers();

app.Run();
