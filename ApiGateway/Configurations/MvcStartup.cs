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
            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddHealthChecks();

            services.AddOcelot();

            
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

            // app.Map("/services", MapApiGateway);
            app.UseHttpsRedirection();
            app.UseOcelot().Wait();

            app.UseHealthChecks("/health");
            return app;
        }

        private static void MapApiGateway(IApplicationBuilder app) 
        {
            //app.UseHttpsRedirection();
            app.UseOcelot().Wait();
        }
    }
}
