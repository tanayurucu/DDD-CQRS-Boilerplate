using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Application.Common.Interfaces.Persistence.Repositories;
using CleanArchitecture.Persistence.Caching;
using CleanArchitecture.Persistence.Common.Constants;
using CleanArchitecture.Persistence.Common.Interceptors;
using CleanArchitecture.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInterceptors();

        services.AddDbContext<CleanArchitectureDbContext>((serviceProvider, options) =>
        {
            var updateAuditableEntitiesInterceptor =
                serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>();
            var updateSoftDeletableEntitiesInterceptor =
                serviceProvider.GetRequiredService<UpdateSoftDeletableEntitiesInterceptor>();
            options.UseSqlServer(configuration.GetConnectionString(ConnectionStringKeys.Database))
                .AddInterceptors(updateAuditableEntitiesInterceptor, updateSoftDeletableEntitiesInterceptor);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddRepositories();

        services.AddCaching(configuration);

        return services;
    }

    private static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddSingleton<UpdateSoftDeletableEntitiesInterceptor>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        return services;
    }

    private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CacheSettings>(configuration.GetSection(CacheSettings.SectionName));

        services.AddStackExchangeRedisCache(options =>
            options.Configuration = configuration.GetConnectionString(ConnectionStringKeys.Redis));

        services.AddScoped<ICacheService, CacheService>();

        return services;
    }
}