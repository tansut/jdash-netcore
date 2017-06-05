using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Api
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application to use jdash.
        /// <typeparam name="ConfiguratorType">Configurator Type, this must be implemented by developers to fit their application.</typeparam>
        /// </summary>
        public static IApplicationBuilder UseJDash<ConfiguratorType>(this IApplicationBuilder app)
            where ConfiguratorType : BaseJDashConfigurator
        {
            return UseJDash<ConfiguratorType>(app, "/jdash/api/v1");
        }

        /// <summary>
        /// Configure the application to use jdash.
        /// </summary>
        /// <typeparam name="ConfiguratorType">Configurator Type, this must be implemented by developers to fit their application.</typeparam>
        /// <param name="app">The application.</param>
        /// <param name="apiPath">The API path of http request to listen.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseJDash<ConfiguratorType>(this IApplicationBuilder app, string apiPath)
           where ConfiguratorType : BaseJDashConfigurator
        {

            IApplicationBuilder newAppConfiguration = null;

            app.Map(new PathString(apiPath), (configuration) =>
            {

#if !(REGISTERED_VERSION)
                configuration.MapWhen(IsNotLocalUrl, WriteUnlicensedResponse);
#endif

                configuration.UseMvc();

                Configuration.ConfiguratorType = typeof(ConfiguratorType);
                newAppConfiguration = configuration;

            });

            return newAppConfiguration;
        }

        private static bool IsNotLocalUrl(HttpContext context)
        {
            var isNotLocal = !(context.Request.Host.Host == "localhost" || context.Request.Host.Host == "127.0.0.1");
            return isNotLocal;
        }

        private static void WriteUnlicensedResponse(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Trial Version of JDash Can Only Be Run At \"localhost\" or \"127.0.0.1\" adresses");
            });
        }

    }
}
