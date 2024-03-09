

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<DataClient>()
            .AddSingleton<PersonService>()
            .AddSingleton<ProductsService>()
            .AddSingleton<SalesService>();

        return services;
    }
}
