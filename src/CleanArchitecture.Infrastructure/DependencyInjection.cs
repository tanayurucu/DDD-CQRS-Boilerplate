using CleanArchitecture.Application.Common.Interfaces.Infrastructure;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Authentication.Constants;
using CleanArchitecture.Infrastructure.Authentication.Policies;
using CleanArchitecture.Infrastructure.Common;
using CleanArchitecture.Infrastructure.Email;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddAuth(configuration);

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationSettings = new List<AuthenticationSettings>();
        configuration.GetSection(AuthenticationSettings.SecitonName).Bind(authenticationSettings);

        if (authenticationSettings.Any())
        {
            foreach (var authenticationSetting in authenticationSettings)
            {
                services.AddAuthentication().AddJwtBearer(authenticationSetting.Name, options =>
                {
                    options.MetadataAddress = authenticationSetting.MetadataAddress;
                    options.Authority = authenticationSetting.Authority;
                    options.Audience = authenticationSetting.Audience;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });
            }

            services.AddAuthorization(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();
                foreach (var authenticationSetting in authenticationSettings)
                {
                    policy.AddAuthenticationSchemes(authenticationSetting.Name);
                }

                options.DefaultPolicy = policy.Build();

                options.AddPolicy(
                    PolicyNames.RequireDeveloperPolicy,
                    policyBuilder =>
                    {
                        policyBuilder.AddRequirements(new DeveloperRequirement());
                        policyBuilder.RequireAuthenticatedUser();
                        foreach (var authenticationSetting in authenticationSettings)
                        {
                            policyBuilder.AddAuthenticationSchemes(authenticationSetting.Name);
                        }
                    }
                );

                options.AddPolicy(
                    PolicyNames.RequireExpertPolicy,
                    policyBuilder =>
                    {
                        policyBuilder.AddRequirements(new ExpertRequirement());
                        policyBuilder.RequireAuthenticatedUser();
                        foreach (var authenticationSetting in authenticationSettings)
                        {
                            policyBuilder.AddAuthenticationSchemes(authenticationSetting.Name);
                        }
                    }
                );

                options.AddPolicy(
                    PolicyNames.RequireOperatorPolicy,
                    policyBuilder =>
                    {
                        policyBuilder.AddRequirements(new OperatorRequirement());
                        policyBuilder.RequireAuthenticatedUser();
                        foreach (var authenticationSetting in authenticationSettings)
                        {
                            policyBuilder.AddAuthenticationSchemes(authenticationSetting.Name);
                        }
                    }
                );

                options.AddPolicy(
                    PolicyNames.RequireUserPolicy,
                    policyBuilder =>
                    {
                        policyBuilder.AddRequirements(new UserRequirement());
                        policyBuilder.RequireAuthenticatedUser();
                        foreach (var authenticationSetting in authenticationSettings)
                        {
                            policyBuilder.AddAuthenticationSchemes(authenticationSetting.Name);
                        }
                    }
                );
            });
        }


        return services;
    }

    public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }
}