using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compuletra.ApiGateway.Configurations
{
    public static class MvcStartup
    {
        public static IServiceCollection AddWebModule(this IServiceCollection services) 
        {
            services.AddHttpContextAccessor();

            services.AddHealthChecks();

            services.AddControllers();
            return services;
        }

        public static IApplicationBuilder UseApplicationWeb(this IApplicationBuilder app) 
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });

            app.Map("/services", MapApiGateway);

            app.UseHealthChecks("/health");
            return app;
        }

        private static void MapApiGateway(IApplicationBuilder app) 
        {
            app.UseOcelot().Wait();
        }
    }
}
