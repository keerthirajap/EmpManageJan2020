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
using Moq;

namespace IOC.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ServiceIOCModuleTest
    {
        #region Private Variables

        protected IContainer _containerInstancePerLifetimeScope { get; private set; }
        protected IContainer _containerNone { get; private set; }

        private Mock<NLog.Logger> _logger { get; set; }

        #endregion Private Variables

        #region Constructor

        public ServiceIOCModuleTest()
        {
            //Arrange
            this._logger = new Mock<NLog.Logger>();

            var containerBuilderInstancePerLifetimeScope = new ContainerBuilder();

            containerBuilderInstancePerLifetimeScope.Register(c => new AppSetting()).SingleInstance();
            containerBuilderInstancePerLifetimeScope.Register(c => new LogInterceptor(this._logger.Object)).SingleInstance();
            containerBuilderInstancePerLifetimeScope.RegisterModule(new ServiceIOCModule("InstancePerLifetimeScope"));
            containerBuilderInstancePerLifetimeScope.RegisterModule(new RepositoryIOCModule("", "InstancePerLifetimeScope"));

            this._containerInstancePerLifetimeScope = containerBuilderInstancePerLifetimeScope.Build();

            var containerBuilderNone = new ContainerBuilder();
            containerBuilderNone.Register(c => new AppSetting()).SingleInstance();
            containerBuilderNone.Register(c => new LogInterceptor(this._logger.Object)).SingleInstance();
            containerBuilderNone.RegisterModule(new ServiceIOCModule(""));
            containerBuilderNone.RegisterModule(new RepositoryIOCModule("", ""));
            this._containerNone = containerBuilderNone.Build();
        }

        #endregion Constructor

        #region Public Methods

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

            //Act
            var _sharedService = _containerInstancePerLifetimeScope.Resolve<ISharedService>();
            //Assert
            Assert.IsNotNull(_sharedService, "ISharedService is not registered");
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

            //Act
            var _sharedService = _containerNone.Resolve<ISharedService>();
            //Assert
            Assert.IsNotNull(_sharedService, "ISharedService is not registered");
        }

        #endregion Public Methods
    }
}