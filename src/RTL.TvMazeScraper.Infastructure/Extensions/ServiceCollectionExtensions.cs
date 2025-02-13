using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RTL.TvMazeScraper.Infastructure.Configurations;
using RTL.TvMazeScraper.Infastructure.Services;
using RTL.TvMazeScraper.Infastructure.Services.Interfaces;
using System.Reflection;
using RTL.TvMazeScraper.Infastructure.Mappers;
using RTL.TvMazeScraper.Infastructure.Data;
using RTL.TvMazeScraper.Application.Extensions;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Infastructure.Models.Settings;

namespace RTL.TvMazeScraper.Infastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddApplicationServices();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(AutomapperProfile)));
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IShowQueryService, ShowQueryService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureSyncServices(this IServiceCollection services, ILogger logger, IConfiguration configuration)
        {
            services.Configure<TvMazeApiSettings>(configuration.GetSection(nameof(TvMazeApiSettings)));
            services.Configure<RateLimiterSettings>(configuration.GetSection(nameof(RateLimiterSettings)));

            var rateLimiterSettings = configuration.GetSection(nameof(RateLimiterSettings)).Get<RateLimiterSettings>()!;
            services.AddHttpClient<ITvMazeApiService, TvMazeApiService>()
                .AddPolicyHandler(PolicyWrapper.CreateHttpClientPolicy(
                    logger,
                    rateLimiterSettings.RetryCount));

            services.AddSingleton(PolicyWrapper.CreateRateLimiterPolicy(
                logger,
                rateLimiterSettings));

            services.AddScoped<ITvMazeApiService, TvMazeApiService>();
            services.AddScoped<ITvMazeSyncService, TvMazeSyncService>();

            return services;
        }
    }
}
