using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Compuletra.ApiGateway.Configurations
{
    public static class SecurityStartup
    {
        public static IServiceCollection AddSecurityModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecuritySettings>(options => configuration.GetSection("security").Bind(options));
            var securitySettings = services.BuildServiceProvider().GetRequiredService<IOptions<SecuritySettings>>().Value;

            var secret = securitySettings.Authentication.Jwt.Secret;

            var keyBites =
                !string.IsNullOrEmpty(secret)
                ? Encoding.ASCII.GetBytes(secret)
                : Convert.FromBase64String(securitySettings.Authentication.Jwt.Base64Secret);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = false;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBites)
                };
            });
            return services;
        }

        public static IApplicationBuilder UseApplicationSecurity(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
