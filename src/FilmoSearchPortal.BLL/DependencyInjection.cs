using Microsoft.Extensions.DependencyInjection;

namespace FilmoSearchPortal.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayerConfiguration(this IServiceCollection services)
    {
        services.AddDataLayerConfiguration();

        return services;
    }
}