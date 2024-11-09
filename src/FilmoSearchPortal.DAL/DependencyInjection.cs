using FilmoSearchPortal.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmoSearchPortal.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataLayerConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IActorRepository, ActorRepository>();
        services.AddScoped<IFilmRepository, FilmRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        return services;
    }
}