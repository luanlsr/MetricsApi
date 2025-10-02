using MetricsApi.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddApplicationDependencies(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCorsPolicy();

var app = builder.Build();

// Configure middlewares
app.UseSwaggerDocumentation();
app.UseCorsPolicy();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
