using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;

namespace WebApi.Config
{
    /// <summary>
    /// The NSwag configuration class needed to generate the Swagger Json specification and the Swagger Ui
    /// </summary>
    public static class NSwagConfig
    {
        public static void Configure(IServiceCollection services, string title, string description)
        {
            services.AddSwaggerDocument(config =>
            {
                var settings = config.SchemaGenerator.Settings;
                config.AllowNullableBodyParameters = false;
                //settings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
                //settings.DefaultEnumHandling = EnumHandling.String;
                settings.AllowReferencesWithProperties = true;

                config.Title = title;
                config.Description = description;
            });
        }

        /// <summary>
        /// NSwag configuration registration
        /// </summary>
        public static void RegisterNSwagConfig(IApplicationBuilder app, OpenApiSchema schema)
        {
            app.UseOpenApi(settings =>
            {
                settings.Path = "/swagger";
                settings.PostProcess = (document, request) =>
                {
                    // Force https scheme
                    document.Schemes = new List<OpenApiSchema> {schema};
                };
            });

            app.UseSwaggerUi3(settings =>
            {
                settings.ServerUrl = "www.ciao.com";
                settings.DocumentPath = "/swagger";
                settings.Path = "/swagger/ui";
                //settings.GeneratorSettings.DefaultUrlTemplate = "api/{controller}/{id}";
            });
        }
    }
}
