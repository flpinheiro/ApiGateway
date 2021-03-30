using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Compuletra.ApiGateway.Configurations
{
    public static class SwaggerStartup
    {
        public static IServiceCollection AddSwaggerModule(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddSwaggerForOcelot(configuration, (o) =>
            {
                o.GenerateDocsForGatewayItSelf = true;
            });
            return services;
        }

        public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder app)
        {
            app.UseSwaggerForOcelotUI();
            return app;
        } 
    }
}
