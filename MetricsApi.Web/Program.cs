

using MetricsApi.CrossCutting.Settings;
using MetricsApi.IoC;
using MetricsApi.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

var databaseSettings = builder.Configuration
                              .GetSection("DatabaseSettings")
                              .Get<DatabaseSettings>()!;

builder.Services.AddApplicationServices(databaseSettings)
                .AddSwaggerDocumentation()
                .AddCorsPolicy()
                .AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

app.UseSwaggerDocumentation()
   .UseCorsPolicy()
   .UseCustomMiddlewares()
   .UseHttpsRedirection()
   .UseAuthentication()
   .UseAuthorization();

app.MapControllers();
app.Run();
