namespace FilmoSearchPortal.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayerConfiguration(this IServiceCollection services)
    {
        services.AddBusinessLayerConfiguration();

        return services;
    }
}