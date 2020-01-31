namespace EmpManage.IOC
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using Castle.DynamicProxy;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.ServiceConcrete;
    using EmpManage.ServiceInterface;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class ServiceIOCModule : Module
    {
        private readonly string _lifeTime;

        public ServiceIOCModule(string lifeTime)
        {
            this._lifeTime = lifeTime;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (this._lifeTime == "InstancePerLifetimeScope")
            {
                builder
                    .RegisterType<AuthenticationService>().As<IAuthenticationService>()
                    .InstancePerLifetimeScope()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LogInterceptor));
            }
            else
            {
                builder
                    .RegisterType<AuthenticationService>().As<IAuthenticationService>()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LogInterceptor));
            }

            base.Load(builder);
        }
    }
}