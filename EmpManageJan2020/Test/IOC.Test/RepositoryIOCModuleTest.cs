using System;
using Autofac;
using Autofac.Builder;
using CompName.ManageStocks.IOC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;
using System.Threading.Tasks;
using CompName.ManageStocks.RepositoryInterface;
using CompName.ManageStocks.CrossCutting.Logging;
using CompName.ManageStocks.ServiceInterface;
using CompName.ManageStocks.CrossCutting.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace IOC.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class RepositoryIOCModuleTest
    {
        protected IContainer _container { get; private set; }
        private readonly NLog.Logger logger;

        [TestInitialize]
        public void Setup()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register(c => new LogInterceptor(this.logger)).SingleInstance();
            containerBuilder.RegisterModule(new RepositoryIOCModule("", "InstancePerLifetimeScope"));

            this._container = containerBuilder.Build();
        }

        [TestMethod]
        public void ValidateRepositoryRegistrations()
        {
            var _userManagementRepository = _container.Resolve<IUserManagementRepository>();
            Assert.IsNotNull(_userManagementRepository, "UserManagementRepository is not registered");

            var _authenticationRepository = _container.Resolve<IAuthenticationRepository>();
            Assert.IsNotNull(_authenticationRepository, "IAuthenticationRepository is not registered");
        }
    }
}