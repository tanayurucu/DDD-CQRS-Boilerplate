using CleanArchitecture.Presentation.Common.Constants;
using CleanArchitecture.Presentation.Common.Errors;

using Mapster;

using MapsterMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Presentation;

/// <summary>
/// Registers services to IoC container
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers Presentation layer services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSingleton<ProblemDetailsFactory, CleanArchitectureProblemDetailsFactory>();

        services.AddMappings();

        services.AddHttpContextAccessor();

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddCors(options => options.AddPolicy(CorsPolicies.LocalhostCorsPolicy,
            policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

        return services;
    }

    private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(PresentationAssembly.Assembly);
        services.AddSingleton(config);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}