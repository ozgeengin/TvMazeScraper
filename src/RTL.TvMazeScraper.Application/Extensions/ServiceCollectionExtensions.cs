using Microsoft.Extensions.DependencyInjection;
using RTL.TvMazeScraper.Application.Mappers;
using RTL.TvMazeScraper.Application.Services;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using System.Reflection;

namespace RTL.TvMazeScraper.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationProfile)));

            services.AddScoped<IShowService, ShowService>();
            services.AddScoped<ITvMazeApiService, TvMazeApiService>();

            return services;
        }
    }
}
