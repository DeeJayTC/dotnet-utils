using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
namespace TCDev.Utilities.NetCore
{
	public static class CORSSetup
	{
		public static IServiceCollection AddCorsAllowAll(this IServiceCollection services, 
			IWebHostEnvironment Environment, string policyName = "DefaultCorsSetup")
		{
			//######## Auth Setup #####################################
			services.AddCors(options =>
			{
				options.AddPolicy(policyName,
					builder =>
					{

						if (Environment.IsDevelopment())
						{
							builder
										.SetIsOriginAllowedToAllowWildcardSubdomains()
										.AllowAnyOrigin()
										.AllowAnyHeader()
										.AllowAnyMethod();
						}
						else
						{
							builder
										.AllowCredentials()
										.AllowAnyHeader()
										.AllowAnyMethod();
						}

					});
			});
			return services;
		}
		
		public static IServiceCollection AddCors(this IServiceCollection services, 
			Func<string,bool> allowedOrigins, 
			string[] allowedMethods, 
			string[] allowedHeaders, 
			IWebHostEnvironment Environment, 
			string policyName = "DefaultCorsSetup")
		{
			services.AddCors(options =>
			{
				options.AddPolicy(policyName,
					builder =>
					{

						if (Environment.IsDevelopment())
						{
							builder
										.SetIsOriginAllowed(allowedOrigins)
										.WithHeaders(allowedHeaders)
										.WithMethods(allowedMethods)
										.SetIsOriginAllowedToAllowWildcardSubdomains();
						}
						else
						{
							builder
										.SetIsOriginAllowed(allowedOrigins)
										.WithHeaders(allowedHeaders)
										.WithMethods(allowedMethods);
						}

					});
			});
			return services;
		}

		public static IApplicationBuilder UseCors(this IApplicationBuilder app, string policyName = "DefaultCorsSetup")
		{
			app.UseCors(policyName);
			return app;
		}
	}
}
