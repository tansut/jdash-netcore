using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using JDash.NetCore.Api;
using JDash.NetCore.Models;
using Microsoft.AspNetCore.Http;
using JDash.NetCore.Provider.MsSQL;
using JDash.NetCore.Provider.MySQL;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
 
            app.UseJDash<JDashConfigurator>();

        }
    }

    public class JDashConfigurator : BaseJDashConfigurator
    { 

        public JDashConfigurator(HttpContext context, bool ensureTablesCreated) : base(context, ensureTablesCreated)
        {
            this._ensureTablesCreated = true;
        }


        public override JDashPrincipalResult GetJDashPrincipal(string authorizationHeader)
        { 
            return new JDashPrincipalResult() { appid = "1", user = "1" };
        }

        public override IJDashPersistenceProvider GetPersistanceProvider()
        {
            // 127.0.0.1 : 3306   root 
            string connectionString = "Server=localhost;Database=jdash_mysql_demo;Uid=root;Pwd=1234;";
            var provider = new JMySQLProvider(connectionString);
            return provider;
        }
    }
}
