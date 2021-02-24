using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compuletra.ApiGateway.Configurations
{
    public static class SwaggerStartup
    {
        public static IServiceCollection AddSwaggerModule(this IServiceCollection services) 
        {
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "Gateway" });
            });
            return services;
        }

        public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c => 
            {
                c.RouteTemplate = "{documentName}/api-docs";
            });
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/v2/api-docs", "Gateway"));
            return app;
        } 
    }
}
