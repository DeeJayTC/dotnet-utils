// TCDev.de 2022/03/18
// TCDev.Utilities.NetCore.SwaggerSetup.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TCDev.CloudStorage.StartupExtension;

namespace TCDev.Utilities.NetCore.StartupExtension;

public static class SwaggerSetup
{
   /// <summary>
   ///    Add and configure swagger services
   /// </summary>
   /// <param name="services"></param>
   /// <returns></returns>
   public static IServiceCollection SetupSwagger(this IServiceCollection services)
   {
      var swaggerConfig = AppSettings.Instance.Get<SwaggerConfig>("Swagger");

      services.AddSwaggerGen(c =>
      {
         c.SwaggerDoc(swaggerConfig.Version,
            new OpenApiInfo
            {
               Title = swaggerConfig.Name, Version = swaggerConfig.Version, Description = swaggerConfig.Description, Contact = new OpenApiContact
               {
                  Email = swaggerConfig.ContactEmail, Name = swaggerConfig.ContactName, Url = new Uri(swaggerConfig.WebsiteUrl)
               }
            });

         c.OperationFilter<SwaggerAddRequiredParameters>();


         // Set the comments path for the Swagger JSON and UI.
         var basePath = PlatformServices.Default.Application.ApplicationBasePath;
         var xmlPath = Path.Combine(basePath, $"{Assembly.GetExecutingAssembly().GetName()}.xml");
         c.IncludeXmlComments(xmlPath);
      });
      return services;
   }


   public static IApplicationBuilder UseSwaggerConfigured(this IApplicationBuilder app)
   {
      var swaggerConfig = AppSettings.Instance.Get<SwaggerConfig>("Swagger");

      app.UseSwagger();
      app.UseSwaggerUI(
         c =>
            c.SwaggerEndpoint(
               swaggerConfig.Endpoint,
               swaggerConfig.Name
            ));


      return app;
   }
}