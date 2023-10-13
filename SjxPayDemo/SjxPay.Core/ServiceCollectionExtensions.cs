using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SjxPay.Core.Geteways;
using SjxPay.Core.Utils;
using System;

namespace SjxPay.Core
{
    public static class ServiceCollectionExtensions
    {
       
        /// <summary>
        /// 添加PaySharp
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        public static void AddSjxPay(this IServiceCollection services, Action<IGateways> setupAction)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IGateways>(a =>
            {
                var gateways = new Gateways();
                setupAction(gateways);

                return gateways;
            });
        }

        /// <summary>
        /// 使用PaySharp
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSjxPay(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpUtil.Configure(httpContextAccessor);

            return app;
        }
    }
}
