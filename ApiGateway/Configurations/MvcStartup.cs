using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Compuletra.ApiGateway.Configurations
{
    public static class MvcStartup
    {
        public static IServiceCollection AddWebModule(this IServiceCollection services) 
        {
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddHttpContextAccessor();
            services.AddHealthChecks();
            services.AddOcelot();
            return services;
        }

        public static IApplicationBuilder UseApplicationWeb(this IApplicationBuilder app) 
        {
            app.UseRouting();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseOcelot().Wait();
            return app;
        }
    }
}
