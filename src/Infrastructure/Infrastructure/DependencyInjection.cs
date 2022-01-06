using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ProjectContext>(opt => opt.UseInMemoryDatabase("MovieRecommendationDatabase"));

            services.AddScoped<IProjectContext>(provider => provider.GetService<ProjectContext>());

            return services;
        }
    }
}
