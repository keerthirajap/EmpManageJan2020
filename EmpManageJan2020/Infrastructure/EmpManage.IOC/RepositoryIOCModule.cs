namespace EmpManage.IOC
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Text;
    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.RepositoryInterface;
    using Insight.Database;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
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
            }
            else
            {
                builder
                    .Register(b => this._sqlConnection.AsParallel<IAuthenticationRepository>())
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LogInterceptor));
            }

            base.Load(builder);
        }
    }
}