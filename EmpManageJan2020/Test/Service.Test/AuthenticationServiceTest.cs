using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using CompName.ManageStocks.CrossCutting.Configuration;
using CompName.ManageStocks.Domain.Admin;
using CompName.ManageStocks.Domain.Authentication;
using CompName.ManageStocks.RepositoryInterface;
using CompName.ManageStocks.ServiceConcrete;
using CompName.ManageStocks.ServiceInterface;

using FizzWare.NBuilder;

using Insight.Database;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Service.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AuthenticationServiceTest
    {
        #region Private Variables

        private Mock<IAuthenticationRepository> _authenticationRepository { get; set; }
        private AppSetting _appSetting { get; set; }
        private AuthenticationService _authenticationService { get; set; }

        private List<User> Users { get; set; }

        private List<UserLogin> UserLogins { get; set; }

        private List<UserRole> UserRoles { get; set; }

        #endregion Private Variables

        #region Constructor

        public AuthenticationServiceTest()
        {
            this._authenticationRepository = new Mock<IAuthenticationRepository>();
            this._appSetting = Builder<AppSetting>.CreateListOfSize(1).Build().ToList().FirstOrDefault();
            this._appSetting.AuthenticationSetting = Builder<AuthenticationSetting>.CreateListOfSize(1).Build().ToList().FirstOrDefault();
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
            this._authenticationRepository
                .Setup(m => m.RegisterUserAsync(this.Users.FirstOrDefault()))
                .ReturnsAsync(this.Users.FirstOrDefault());

            _authenticationService = new AuthenticationService(this._appSetting, this._authenticationRepository.Object);

            //Act
            var user = await this._authenticationService.RegisterUserAsync(this.Users.FirstOrDefault());

            //Assert
            Assert.IsTrue(user != null);
            Assert.IsTrue(user.UserId > 0);
        }

        [TestMethod]
        public async Task CanRunIsUserNameExistsAsync()
        {
            //Arrange
            this._authenticationRepository
             .Setup(m => m.GetUserNameForNewUserValidationAsync(It.IsAny<string>()))
             .ReturnsAsync(this.Users.Select(s => s.UserName).ToList());

            _authenticationService = new AuthenticationService(this._appSetting, this._authenticationRepository.Object);

            //Act
            var isUserNameExistsAsync = await this._authenticationService.IsUserNameExistsAsync(this.Users.FirstOrDefault().UserName);

            //Assert
            Assert.IsTrue(isUserNameExistsAsync == true);
        }

        [TestMethod]
        public async Task CanRunIsUserNameExistsAsync_Negative()
        {
            //Arrange
            this._authenticationRepository
             .Setup(m => m.GetUserNameForNewUserValidationAsync(It.IsAny<string>()))
             .ReturnsAsync(new List<string>());

            _authenticationService = new AuthenticationService(this._appSetting, this._authenticationRepository.Object);

            //Act
            var isUserNameExistsAsync = await this._authenticationService.IsUserNameExistsAsync(this.Users.FirstOrDefault().UserName);

            //Assert
            Assert.IsFalse(isUserNameExistsAsync);
        }

        [TestMethod]
        public async Task CanRunIsEmailIdExistsAsync()
        {
            //Arrange
            this._authenticationRepository
             .Setup(m => m.GetEmailIdForNewUserValidationAsync(It.IsAny<string>()))
             .ReturnsAsync(this.Users.Select(s => s.EmailId).ToList());

            _authenticationService = new AuthenticationService(this._appSetting, this._authenticationRepository.Object);

            //Act
            var isEmailIdExistsAsync = await this._authenticationService.IsEmailIdExistsAsync(this.Users.FirstOrDefault().EmailId);

            //Assert
            Assert.IsTrue(isEmailIdExistsAsync == true);
        }

        [TestMethod]
        public async Task CanRunIsEmailIdExistsAsync_Negative()
        {
            //Arrange
            this._authenticationRepository
             .Setup(m => m.GetEmailIdForNewUserValidationAsync(It.IsAny<string>()))
             .ReturnsAsync(new List<string>());

            _authenticationService = new AuthenticationService(this._appSetting, this._authenticationRepository.Object);

            //Act
            var isEmailIdExistsAsync = await this._authenticationService.IsEmailIdExistsAsync(this.Users.FirstOrDefault().EmailId);

            //Assert
            Assert.IsFalse(isEmailIdExistsAsync);
        }

        #endregion Register User

        #region User Login

        [TestMethod]
        public async Task CanValidateUserLoginAsync()
        {
            //Arrange
            this._authenticationRepository
             .Setup(m => m.GetUserDetailsForLoginValidationAsync(It.IsAny<string>()))
             .ReturnsAsync(new Results<User, UserRole>());

            this._authenticationRepository
             .Setup(m => m.SaveUserLoggingDetailsAsync(this.UserLogins.FirstOrDefault(), It.IsAny<bool>()))
             ;

            _authenticationService = new AuthenticationService(this._appSetting, this._authenticationRepository.Object);

            //Act
            var actResults = await this._authenticationService.ValidateUserLoginAsync(this.UserLogins.FirstOrDefault());

            //Assert
            Assert.IsTrue(actResults.userLogin.UserId == 0);
            Assert.IsTrue(actResults.userRoles.Count == 0);
        }

        [TestMethod]
        public async Task CanValidateUserLoginAsync_Negative()
        {
            //Arrange
            this._authenticationRepository
             .Setup(m => m.GetUserDetailsForLoginValidationAsync(It.IsAny<string>()))
             .ReturnsAsync(new Results<User, UserRole>());

            this._authenticationRepository
             .Setup(m => m.SaveUserLoggingDetailsAsync(this.UserLogins.FirstOrDefault(), It.IsAny<bool>()))
             ;

            _authenticationService = new AuthenticationService(this._appSetting, this._authenticationRepository.Object);

            //Act
            var actResults = await this._authenticationService.ValidateUserLoginAsync(this.UserLogins.FirstOrDefault());

            //Assert
            Assert.IsTrue(actResults.userLogin.UserId == 0);
            Assert.IsTrue(actResults.userRoles.Count == 0);
        }

        #endregion User Login

        #endregion Public Methods
    }
}