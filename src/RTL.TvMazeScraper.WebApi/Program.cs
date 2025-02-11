using Hangfire;
using RTL.TvMazeScraper.Application.Extensions;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Infastructure.Extensions;
using RTL.TvMazeScraper.WebApi.Extensions;
using RTL.TvMazeScraper.WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile($"appsettings.json")
    .Build();

var logger = LoggerFactory.Create(logging =>
{
    logging.ClearProviders();
    logging.AddConfiguration(configuration.GetSection("Logging"));
}).CreateLogger("Logger");

var connectionString = configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(logger, configuration, connectionString);
builder.Services.AddWebServices(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<ITvMazeJobService>("TvMazeSync", service => service.SyncShowsAsync(), Cron.Hourly);

app.Run();
