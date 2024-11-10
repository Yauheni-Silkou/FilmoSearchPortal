using FilmoSearchPortal.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FilmoSearchPortal.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayerConfiguration(this IServiceCollection services)
    {
        services.AddDataLayerConfiguration();

        services.AddScoped<IActorService, ActorService>();
        services.AddScoped<IFilmService, FilmService>();
        services.AddScoped<IReviewService, ReviewService>();

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        return services;
    }
}