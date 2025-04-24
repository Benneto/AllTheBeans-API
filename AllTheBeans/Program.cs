using AllTheBeans.Application;
using AllTheBeans.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register services
builder.Services
    .AddApplication()
    .AddDataAccess(connectionString);

builder.Services.AddControllers();

// Register Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AllTheBeans API",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AllTheBeans API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
