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
    public class ServiceIOCModuleTest
    {
        protected IContainer _containerInstancePerLifetimeScope { get; private set; }
        protected IContainer _containerNone { get; private set; }

        private readonly NLog.Logger logger;

        [TestInitialize]
        public void Setup()
        {
            //Arrange

            var containerBuilderInstancePerLifetimeScope = new ContainerBuilder();

            containerBuilderInstancePerLifetimeScope.Register(c => new AppSetting()).SingleInstance();
            containerBuilderInstancePerLifetimeScope.Register(c => new LogInterceptor(this.logger)).SingleInstance();
            containerBuilderInstancePerLifetimeScope.RegisterModule(new ServiceIOCModule("InstancePerLifetimeScope"));
            containerBuilderInstancePerLifetimeScope.RegisterModule(new RepositoryIOCModule("", "InstancePerLifetimeScope"));

            this._containerInstancePerLifetimeScope = containerBuilderInstancePerLifetimeScope.Build();

            var containerBuilderNone = new ContainerBuilder();
            containerBuilderNone.Register(c => new AppSetting()).SingleInstance();
            containerBuilderNone.Register(c => new LogInterceptor(this.logger)).SingleInstance();
            containerBuilderNone.RegisterModule(new ServiceIOCModule(""));
            containerBuilderNone.RegisterModule(new RepositoryIOCModule("", ""));
            this._containerNone = containerBuilderNone.Build();
        }

        [TestMethod]
        public void ValidateServiceRegistrations_InstancePerLifetimeScope()
        {
            //Act
            var _userManagementService = _containerInstancePerLifetimeScope.Resolve<IUserManagementService>();
            //Assert
            Assert.IsNotNull(_userManagementService, "UserManagementService is not registered");

            //Act
            var _authenticationService = _containerInstancePerLifetimeScope.Resolve<IAuthenticationService>();
            //Assert
            Assert.IsNotNull(_authenticationService, "IAuthenticationService is not registered");

            //Act
            var _productManagementService = _containerInstancePerLifetimeScope.Resolve<IProductManagementService>();
            //Assert
            Assert.IsNotNull(_productManagementService, "IProductManagementService is not registered");
        }

        [TestMethod]
        public void ValidateServiceRegistrations_None()
        {
            //Act
            var _userManagementService = _containerNone.Resolve<IUserManagementService>();
            //Assert
            Assert.IsNotNull(_userManagementService, "UserManagementService is not registered");

            //Act
            var _authenticationService = _containerNone.Resolve<IAuthenticationService>();
            //Assert
            Assert.IsNotNull(_authenticationService, "IAuthenticationService is not registered");

            //Act
            var _productManagementService = _containerNone.Resolve<IProductManagementService>();
            //Assert
            Assert.IsNotNull(_productManagementService, "IProductManagementService is not registered");
        }
    }
}