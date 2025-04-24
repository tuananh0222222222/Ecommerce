using ecommerce.infrastructure.DependencyInjection;
using ecommerce.infrastructure.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//infrastructure
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddRedisServices(builder.Configuration);
SerilogLogger.ConfigureLogger(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()
    )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction()
    )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    await app.RunAsync();
    Log.Information("Application started successfully");
}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
    await app.StopAsync();
}
finally
{
    await app.DisposeAsync();
    await Log.CloseAndFlushAsync();
}