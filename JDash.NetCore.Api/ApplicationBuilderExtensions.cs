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
        public static IApplicationBuilder UseJDash<ConfiguratorType>(this IApplicationBuilder app)
            where ConfiguratorType : BaseJDashConfigurator
        {
            return UseJDash<ConfiguratorType>(app, "/jdash/api/v1");
        }

        public static IApplicationBuilder UseJDash<ConfiguratorType>(this IApplicationBuilder app, string apiPath)
           where ConfiguratorType : BaseJDashConfigurator
        {
            IApplicationBuilder newAppConfiguration = null;
            app.Map(new PathString(apiPath), (configuration) =>
            {
                Configuration.ConfiguratorType = typeof(ConfiguratorType);
                configuration.UseMvc();
                newAppConfiguration = configuration;
            });

            return newAppConfiguration;
        }
    }
}
