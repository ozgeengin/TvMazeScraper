using Hangfire;
using RTL.TvMazeScraper.WebApi.Services;
using RTL.TvMazeScraper.WebApi.Services.Interfaces;

namespace RTL.TvMazeScraper.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ITvMazeJobService, TvMazeJobService>();

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString));

            services.AddHangfireServer();

            return services;
        }
    }
}
