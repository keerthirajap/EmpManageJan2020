namespace CompName.ManageStocks.IOC
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using CompName.ManageStocks.CrossCutting.Logging;
    using CompName.ManageStocks.RepositoryInterface;
    using Insight.Database;

    //Follow anti-pattern only

    public class RepositoryIOCModule : Module
    {
        private readonly DbConnection _sqlConnection;
        private readonly string _lifeTime;

        public RepositoryIOCModule(string sqlConnectionString, string lifeTime)
        {
            this._sqlConnection = new SqlConnection(sqlConnectionString);
            this._lifeTime = lifeTime;
        }

        protected override void Load(ContainerBuilder builder)
        {
            SqlInsightDbProvider.RegisterProvider();

            if (this._lifeTime == "InstancePerLifetimeScope")
            {
                builder
                    .Register(b => this._sqlConnection.AsParallel<IAuthenticationRepository>())
                    .InstancePerLifetimeScope()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LogInterceptor));

                builder
                   .Register(b => this._sqlConnection.AsParallel<IUserManagementRepository>())
                   .InstancePerLifetimeScope()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(LogInterceptor));

                builder
                  .Register(b => this._sqlConnection.AsParallel<IProductManagementRepository>())
                  .InstancePerLifetimeScope()
                  .EnableInterfaceInterceptors()
                  .InterceptedBy(typeof(LogInterceptor));

                builder
                 .Register(b => this._sqlConnection.AsParallel<ISharedRepository>())
                 .InstancePerLifetimeScope()
                 .EnableInterfaceInterceptors()
                 .InterceptedBy(typeof(LogInterceptor));
            }
            else
            {
                builder
                    .Register(b => this._sqlConnection.AsParallel<IAuthenticationRepository>())
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LogInterceptor));

                builder
                    .Register(b => this._sqlConnection.AsParallel<IUserManagementRepository>())
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LogInterceptor));

                builder
                .Register(b => this._sqlConnection.AsParallel<IProductManagementRepository>())
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LogInterceptor));

                builder
               .Register(b => this._sqlConnection.AsParallel<ISharedRepository>())
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(LogInterceptor));
            }

            base.Load(builder);
        }
    }
}