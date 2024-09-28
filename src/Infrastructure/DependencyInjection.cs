namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services
            .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
            .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        return services;
    }
}