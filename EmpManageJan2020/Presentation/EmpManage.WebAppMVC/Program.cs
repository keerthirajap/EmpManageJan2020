namespace EmpManage.WebAppMVC
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using NLog.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class Program
    {
#pragma warning disable SA1306 // Field names should begin with lower-case letter
#pragma warning disable SA1310 // Field names should not contain underscore
        private static string ASPNETCORE_ENVIRONMENT;
#pragma warning restore SA1310 // Field names should not contain underscore
#pragma warning restore SA1306 // Field names should begin with lower-case letter

        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                var envSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "enviromentSettings.json");
                var envSettings = JObject.Parse(File.ReadAllText(envSettingsPath));
                ASPNETCORE_ENVIRONMENT = envSettings["ASPNETCORE_ENVIRONMENT"].ToString();

                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //Make sure envsettings.json is setup with Debug | Development | UAT | Production
                    // If none is set it use Operative System hosting enviroment
                    if (!string.IsNullOrWhiteSpace(ASPNETCORE_ENVIRONMENT))
                    {
                        webBuilder.UseEnvironment(ASPNETCORE_ENVIRONMENT);
                    }
                    else
                    {
                        webBuilder.UseEnvironment("Debug");
                    }

                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
            .UseNLog();  // NLog: Setup NLog for Dependency injection
    }
}