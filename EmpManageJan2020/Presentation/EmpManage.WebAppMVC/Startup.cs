namespace EmpManage.WebAppMVC
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ElmahCore.Mvc;
    using EmpManage.WebAppMVC.Infrastructure.CustomFilters;
    using Autofac;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.IOC;
    using Castle.DynamicProxy;
    using Autofac.Extensions.DependencyInjection;
    using AutoMapper;
    using EmpManage.WebAppMVC.Infrastructure.AutoMapper;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class Startup
    {
        private readonly NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();
            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddElmah();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(LoggingActionFilter));
            }).AddNewtonsoftJson();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddOptions();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(c => new LogInterceptor(this.logger)).SingleInstance();
            builder.RegisterModule(new RepositoryIOCModule("Data Source=.;Initial Catalog=EmpManage;Integrated Security=True", "InstancePerLifetimeScope"));
            builder.RegisterModule(new ServiceIOCModule("InstancePerLifetimeScope"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.Use(
                next =>
                {
                    return async context =>
                    {
                        var stopWatch = new Stopwatch();
                        stopWatch.Start();
                        context.Response.OnStarting(
                            () =>
                            {
                                context.Response.Headers.Add("RequestId", context.TraceIdentifier);
                                stopWatch.Stop();

                                context.Response.Headers.Add("X-ResponseTime-Ms", stopWatch.ElapsedMilliseconds.ToString());
                                return Task.CompletedTask;
                            });

                        await next(context);
                    };
                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/elmah", StringComparison.OrdinalIgnoreCase), appBuilder =>
            {
                appBuilder.Use(next =>
                {
                    return async ctx =>
                    {
                        ctx.Features.Get<IHttpBodyControlFeature>().AllowSynchronousIO = true;

                        await next(ctx);
                    };
                });
            });
            app.UseElmah();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}