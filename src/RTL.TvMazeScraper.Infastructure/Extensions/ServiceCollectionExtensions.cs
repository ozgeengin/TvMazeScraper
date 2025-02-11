using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RTL.TvMazeScraper.Application.Repositories;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Infastructure.Configurations;
using RTL.TvMazeScraper.Infastructure.Data;
using RTL.TvMazeScraper.Infastructure.Repositories;
using RTL.TvMazeScraper.Infastructure.Services;
using RTL.TvMazeScraper.Infastructure.Settings;

namespace RTL.TvMazeScraper.Infastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ILogger logger, IConfiguration configuration, string connectionString)
        {
            services.Configure<TvMazeApiSettings>(configuration.GetSection(nameof(TvMazeApiSettings)));
            services.Configure<CircuitBreakerSettings>(configuration.GetSection(nameof(CircuitBreakerSettings)));
            services.Configure<RateLimiterSettings>(configuration.GetSection(nameof(RateLimiterSettings)));

            services.AddHttpClient<ITvMazeApiService, TvMazeApiService>()
                .AddPolicyHandler(PolicyWrapper.CreateHttpClientPolicy(
                    logger,
                    configuration.GetSection(nameof(CircuitBreakerSettings)).Get<CircuitBreakerSettings>()!));

            services.AddSingleton(PolicyWrapper.CreateRateLimiterPolicy(
                logger,
                configuration.GetSection(nameof(RateLimiterSettings)).Get<RateLimiterSettings>()!));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IShowRepository, ShowRepository>();
            services.AddScoped<ITvMazeApiService, TvMazeApiService>();

            return services;
        }
    }
}
