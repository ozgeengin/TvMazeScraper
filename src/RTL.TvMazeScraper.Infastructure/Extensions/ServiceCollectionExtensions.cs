using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RTL.TvMazeScraper.Application.Repositories;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Infastructure.Data;
using RTL.TvMazeScraper.Infastructure.Repositories;
using RTL.TvMazeScraper.Infastructure.Services;

namespace RTL.TvMazeScraper.Infastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
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
