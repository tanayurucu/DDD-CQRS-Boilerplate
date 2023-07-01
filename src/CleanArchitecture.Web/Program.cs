using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Persistence;
using CleanArchitecture.Presentation;
using CleanArchitecture.Presentation.Common.Constants;
using CleanArchitecture.Web;

using Microsoft.AspNetCore.Mvc.ApiExplorer;

using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation();

    builder.Services.AddPersistence(builder.Configuration);

    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddApplication();

    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

    builder.Services.AddSwaggerGen();

    builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

    builder.Services.AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var groupName in apiVersionDescriptionProvider.ApiVersionDescriptions.Select(description =>
                         description.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
            }
        });
    }

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseCors(CorsPolicies.LocalhostCorsPolicy);

    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.UseStaticFiles();

    app.MapControllers();
}

app.Run();