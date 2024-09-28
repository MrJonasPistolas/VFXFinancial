

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    internal static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ThirdPartyConfiguration>(configuration.GetSection(nameof(ThirdPartyConfiguration)));
        return services;
    }

    internal static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "VFX Financial",
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
        });
    }

    internal static IServiceCollection AddApiHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration.GetSection(nameof(ThirdPartyConfiguration)).GetValue<string>("Url");

        services.AddHttpClient("api", c =>
        {
            c.BaseAddress = new Uri(url!);
        });

        return services;
    }

    internal static IServiceCollection AddSerialization(this IServiceCollection services)
    {
        services
            .AddScoped<IJsonSerializerOptions, SystemTextJsonOptions>()
            .Configure<SystemTextJsonOptions>(configureOptions =>
            {
                if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                    configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
            });
        services.AddScoped<IJsonSerializerSettings, NewtonsoftJsonSettings>();

        services.AddScoped<IJsonSerializer, SystemTextJsonSerializer>(); // you can change it
        return services;
    }

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
}