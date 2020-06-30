using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using CompName.ManageStocks.Domain.Admin;
using CompName.ManageStocks.Domain.Authentication;
using CompName.ManageStocks.RepositoryInterface;
using FizzWare.NBuilder;
using Insight.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Repository.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AuthenticationRepositoryTest
    {
        #region Private Variables

        private Mock<IAuthenticationRepository> _authenticationRepository { get; set; }

        private List<User> Users { get; set; }

        private List<UserLogin> UserLogins { get; set; }

        private List<UserRole> UserRoles { get; set; }

        #endregion Private Variables

        #region Constructor

        public AuthenticationRepositoryTest()
        {
            this._authenticationRepository = new Mock<IAuthenticationRepository>();
            this.Users = Builder<User>.CreateListOfSize(100).Build().ToList();
            this.UserLogins = Builder<UserLogin>.CreateListOfSize(100).Build().ToList();
            this.UserRoles = Builder<UserRole>.CreateListOfSize(100).Build().ToList();
        }

        #endregion Constructor

        #region Public Methods

        #region Register User

        [TestMethod]
        public async Task CanRegisterUserAsync()
        {
            //Arrange
            this._authenticationRepository.Setup(m => m.RegisterUserAsync(It.IsAny<User>())).ReturnsAsync(this.Users.FirstOrDefault());

            //Act
            var user = await this._authenticationRepository.Object.RegisterUserAsync(new User());

            //Assert
            Assert.IsTrue(user.UserId == 1);
        }

        [TestMethod]
        public async Task CanGetUserNameForNewUserValidationAsync()
        {
            //Arrange
            this._authenticationRepository.Setup(m => m.GetUserNameForNewUserValidationAsync(It.IsAny<string>())).ReturnsAsync(this.Users.Select(s => s.UserName).ToList());

            //Act
            var userNames = await this._authenticationRepository.Object.GetUserNameForNewUserValidationAsync("");

            //Assert
            Assert.IsTrue(userNames.Count > 1);
        }

        [TestMethod]
        public async Task CanGetEmailIdForNewUserValidationAsync()
        {
            //Arrange
            this._authenticationRepository.Setup(m => m.GetEmailIdForNewUserValidationAsync(It.IsAny<string>())).ReturnsAsync(this.Users.Select(s => s.EmailId).ToList());

            //Act
            var emailIds = await this._authenticationRepository.Object.GetEmailIdForNewUserValidationAsync("");

            //Assert
            Assert.IsTrue(emailIds.Count > 1);
        }

        #endregion Register User

        #region User Login

        [TestMethod]
        public async Task CanGetUserDetailsForLoginValidationAsync()
        {
            //Arrange
            this._authenticationRepository
                .Setup(m => m.GetUserDetailsForLoginValidationAsync(It.IsAny<string>()))
                .ReturnsAsync(new Results<User, UserRole>());

            //Act
            var results = await this._authenticationRepository.Object.GetUserDetailsForLoginValidationAsync("");

            //Assert
            Assert.IsTrue(results.Set1 == null);
            Assert.IsTrue(results.Set2 == null);
        }

        [TestMethod]
        public async Task CanSaveUserLoggingDetailsAsync()
        {
            //Arrange

            this._authenticationRepository
                .Setup(m => m.SaveUserLoggingDetailsAsync(It.IsAny<UserLogin>(), It.IsAny<bool>()));

            //Act
            await this._authenticationRepository.Object.SaveUserLoggingDetailsAsync(new UserLogin(), true);

            //Assert
            Assert.IsTrue(true);
        }

        #endregion User Login

        #endregion Public Methods
    }
}