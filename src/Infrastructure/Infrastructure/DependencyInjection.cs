using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MovieRecommendationDatabase"));
            });

            services.AddScoped<IProjectContext>(provider => provider.GetService<ProjectContext>());

            return services;
        }
    }
}
