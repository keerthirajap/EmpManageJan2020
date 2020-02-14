namespace CompName.ManageStocks.IOC
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using Castle.DynamicProxy;
    using CompName.ManageStocks.CrossCutting.Logging;
    using CompName.ManageStocks.ServiceConcrete;
    using CompName.ManageStocks.ServiceInterface;

    //Follow anti-pattern only

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
                builder
                    .RegisterType<UserManagementService>().As<IUserManagementService>()
                    .InstancePerLifetimeScope()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LogInterceptor));
                builder
                    .RegisterType<ProductManagementService>().As<IProductManagementService>()
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
                builder
                   .RegisterType<UserManagementService>().As<IUserManagementService>()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(LogInterceptor));
                builder
                    .RegisterType<ProductManagementService>().As<IProductManagementService>()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LogInterceptor));
            }

            base.Load(builder);
        }
    }
}