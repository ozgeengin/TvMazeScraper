using Microsoft.Extensions.DependencyInjection;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Domain.Services.Interfaces;
using RTL.TvMazeScraper.Domain.Services;
using RTL.TvMazeScraper.Application.Services;

namespace RTL.TvMazeScraper.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IShowDomainService, ShowDomainService>();
            services.AddScoped<IShowService, ShowService>();

            return services;
        }
    }
}
