namespace FilmoSearchPortal.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayerConfiguration(this IServiceCollection services)
    {
        services.AddBusinessLayerConfiguration();

        return services;
    }
}