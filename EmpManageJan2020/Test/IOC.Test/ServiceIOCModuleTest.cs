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

namespace IOC.Test
{
    [TestClass]
    public class ServiceIOCModuleTest
    {
        protected IContainer _container { get; private set; }
        private readonly NLog.Logger logger;

        [TestInitialize]
        public void Setup()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register(c => new AppSetting()).SingleInstance();
            containerBuilder.Register(c => new LogInterceptor(this.logger)).SingleInstance();
            containerBuilder.RegisterModule(new ServiceIOCModule("InstancePerLifetimeScope"));
            containerBuilder.RegisterModule(new RepositoryIOCModule("", "InstancePerLifetimeScope"));

            this._container = containerBuilder.Build();
        }

        [TestMethod]
        public void ValidateServiceRegistrations()
        {
            var _userManagementService = _container.Resolve<IUserManagementService>();
            Assert.IsNotNull(_userManagementService, "UserManagementService is not registered");

            var _authenticationService = _container.Resolve<IAuthenticationService>();
            Assert.IsNotNull(_authenticationService, "IAuthenticationService is not registered");
        }
    }
}