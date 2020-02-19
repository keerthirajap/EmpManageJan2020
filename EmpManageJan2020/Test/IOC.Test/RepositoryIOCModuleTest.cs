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
        protected IContainer _containerInstancePerLifetimeScope { get; private set; }

        protected IContainer _containerNone { get; private set; }

        private readonly NLog.Logger logger;

        [TestInitialize]
        public void Setup()
        {
            var containerBuilderInstancePerLifetimeScope = new ContainerBuilder();

            containerBuilderInstancePerLifetimeScope.Register(c => new LogInterceptor(this.logger)).SingleInstance();
            containerBuilderInstancePerLifetimeScope.RegisterModule(new RepositoryIOCModule("", "InstancePerLifetimeScope"));

            this._containerInstancePerLifetimeScope = containerBuilderInstancePerLifetimeScope.Build();

            var containerBuilderNone = new ContainerBuilder();

            containerBuilderNone.Register(c => new LogInterceptor(this.logger)).SingleInstance();
            containerBuilderNone.RegisterModule(new RepositoryIOCModule("", ""));

            this._containerNone = containerBuilderNone.Build();
        }

        [TestMethod]
        public void ValidateRepositoryRegistrations_InstancePerLifetimeScope()
        {
            var _userManagementRepository = _containerInstancePerLifetimeScope.Resolve<IUserManagementRepository>();
            Assert.IsNotNull(_userManagementRepository, "UserManagementRepository is not registered");

            var _authenticationRepository = _containerInstancePerLifetimeScope.Resolve<IAuthenticationRepository>();
            Assert.IsNotNull(_authenticationRepository, "IAuthenticationRepository is not registered");

            var _productManagementRepository = _containerInstancePerLifetimeScope.Resolve<IProductManagementRepository>();
            Assert.IsNotNull(_productManagementRepository, "IProductManagementRepository is not registered");
        }

        [TestMethod]
        public void ValidateRepositoryRegistrations_None()
        {
            var _userManagementRepository = _containerNone.Resolve<IUserManagementRepository>();
            Assert.IsNotNull(_userManagementRepository, "UserManagementRepository is not registered");

            var _authenticationRepository = _containerNone.Resolve<IAuthenticationRepository>();
            Assert.IsNotNull(_authenticationRepository, "IAuthenticationRepository is not registered");

            var _productManagementRepository = _containerNone.Resolve<IProductManagementRepository>();
            Assert.IsNotNull(_productManagementRepository, "IProductManagementRepository is not registered");
        }
    }
}