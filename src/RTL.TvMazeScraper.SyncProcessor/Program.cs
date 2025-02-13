using Hangfire;
using RTL.TvMazeScraper.Infastructure.Extensions;
using RTL.TvMazeScraper.Infastructure.Services.Interfaces;
using RTL.TvMazeScraper.SyncProcessor;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddInfrastructureServices(connectionString);
builder.Services.AddInfrastructureSyncServices(logger, configuration);

builder.Services.AddScoped<ITvMazeJobService, TvMazeJobService>();

builder.Services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseSqlServerStorage(connectionString));

builder.Services.AddHangfireServer(x => x.WorkerCount = 1);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<ITvMazeJobService>(nameof(ITvMazeJobService), s => s.SyncShowsFromApiToDatabaseAsync(), Cron.Hourly);

app.Run();