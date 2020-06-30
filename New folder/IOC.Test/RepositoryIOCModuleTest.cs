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
    public class RepositoryIOCModuleTest
    {
        #region Private Variables

        protected IContainer _containerInstancePerLifetimeScope { get; private set; }

        protected IContainer _containerNone { get; private set; }

        private Mock<NLog.Logger> _logger { get; set; }

        #endregion Private Variables

        #region Constructor

        public RepositoryIOCModuleTest()
        {
            //Arrange
            this._logger = new Mock<NLog.Logger>();

            var containerBuilderInstancePerLifetimeScope = new ContainerBuilder();

            containerBuilderInstancePerLifetimeScope.Register(c => new LogInterceptor(this._logger.Object)).SingleInstance();
            containerBuilderInstancePerLifetimeScope.RegisterModule(new RepositoryIOCModule("", "InstancePerLifetimeScope"));

            this._containerInstancePerLifetimeScope = containerBuilderInstancePerLifetimeScope.Build();

            var containerBuilderNone = new ContainerBuilder();

            containerBuilderNone.Register(c => new LogInterceptor(this._logger.Object)).SingleInstance();
            containerBuilderNone.RegisterModule(new RepositoryIOCModule("", ""));

            this._containerNone = containerBuilderNone.Build();
        }

        #endregion Constructor

        #region Public Methods

        [TestMethod]
        public void ValidateRepositoryRegistrations_InstancePerLifetimeScope()
        {
            //Act
            var _userManagementRepository = _containerInstancePerLifetimeScope.Resolve<IUserManagementRepository>();
            //Assert
            Assert.IsNotNull(_userManagementRepository, "UserManagementRepository is not registered");

            //Act
            var _authenticationRepository = _containerInstancePerLifetimeScope.Resolve<IAuthenticationRepository>();
            //Assert
            Assert.IsNotNull(_authenticationRepository, "IAuthenticationRepository is not registered");

            //Act
            var _productManagementRepository = _containerInstancePerLifetimeScope.Resolve<IProductManagementRepository>();
            //Assert
            Assert.IsNotNull(_productManagementRepository, "IProductManagementRepository is not registered");

            //Act
            var _sharedRepository = _containerInstancePerLifetimeScope.Resolve<ISharedRepository>();
            //Assert
            Assert.IsNotNull(_sharedRepository, "ISharedRepository is not registered");
        }

        [TestMethod]
        public void ValidateRepositoryRegistrations_None()
        {
            //Act
            var _userManagementRepository = _containerNone.Resolve<IUserManagementRepository>();
            //Assert
            Assert.IsNotNull(_userManagementRepository, "UserManagementRepository is not registered");

            //Act
            var _authenticationRepository = _containerNone.Resolve<IAuthenticationRepository>();
            //Assert
            Assert.IsNotNull(_authenticationRepository, "IAuthenticationRepository is not registered");

            //Act
            var _productManagementRepository = _containerNone.Resolve<IProductManagementRepository>();
            //Assert
            Assert.IsNotNull(_productManagementRepository, "IProductManagementRepository is not registered");

            //Act
            var _sharedRepository = _containerNone.Resolve<ISharedRepository>();
            //Assert
            Assert.IsNotNull(_sharedRepository, "ISharedRepository is not registered");
        }

        #endregion Public Methods
    }
}