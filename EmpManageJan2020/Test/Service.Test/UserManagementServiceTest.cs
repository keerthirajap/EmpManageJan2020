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
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Service.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UserManagementServiceTest
    {
        #region Private Variables

        private Mock<IUserManagementRepository> _userManagementRepository { get; set; }
        private AppSetting _appSetting { get; set; }
        private UserManagementService _userManagementService { get; set; }

        private List<User> Users { get; set; }

        private List<UserLogin> UserLogins { get; set; }

        private List<UserRole> UserRoles { get; set; }

        private List<UserGender> UserGenders { get; set; }

        private List<UserTitle> UserTitles { get; set; }

        #endregion Private Variables

        #region TestInitialize

        [TestInitialize]
        public void Setup()
        {
            this._userManagementRepository = new Mock<IUserManagementRepository>(MockBehavior.Strict);
            this._appSetting = Builder<AppSetting>.CreateListOfSize(1).Build().ToList().FirstOrDefault();
            this._appSetting.AuthenticationSetting = Builder<AuthenticationSetting>.CreateListOfSize(1).Build().ToList().FirstOrDefault();
            this.Users = Builder<User>.CreateListOfSize(100).Build().ToList();
            this.UserLogins = Builder<UserLogin>.CreateListOfSize(100).Build().ToList();
            this.UserRoles = Builder<UserRole>.CreateListOfSize(100).Build().ToList();
            this.UserGenders = Builder<UserGender>.CreateListOfSize(10).Build().ToList();
            this.UserTitles = Builder<UserTitle>.CreateListOfSize(10).Build().ToList();
        }

        #endregion TestInitialize

        #region Public Methods

        [TestMethod]
        public async Task CanGetAllUserAccountsAsync()
        {
            this._userManagementRepository.Setup(m => m.GetAllUserAccountsAsync()).ReturnsAsync(this.Users);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            List<User> users = await this._userManagementService.GetAllUserAccountsAsync();

            Assert.IsTrue(users.Count >= 1);
        }

        [TestMethod]
        public async Task CanGetAllUserGenderDetailsAsync()
        {
            this._userManagementRepository.Setup(m => m.GetAllUserGenderDetailsAsync()).ReturnsAsync(this.UserGenders);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            List<UserGender> userGenders = await this._userManagementService.GetAllUserGenderDetailsAsync();
            Assert.IsTrue(userGenders.Count >= 1);
        }

        [TestMethod]
        public async Task CanGetAllUserTitleDetailsAsync()
        {
            this._userManagementRepository
               .Setup(m => m.GetAllUserTitleDetailsAsync())
               .ReturnsAsync(this.UserTitles);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            List<UserTitle> userTitles = await this._userManagementService.GetAllUserTitleDetailsAsync();
            Assert.IsTrue(userTitles.Count >= 1);
        }

        #region Manage User

        [TestMethod]
        public async Task CanGetUserAccountDetailsAsync()
        {
            User userDetails = null;

            this._userManagementRepository
               .Setup(m => m.GetUserAccountDetailsAsync(It.IsAny<long>()))
               .ReturnsAsync(this.Users.FirstOrDefault());

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            userDetails = await this._userManagementService.GetUserAccountDetailsAsync(5);

            Assert.IsTrue(userDetails != null);
        }

        [TestMethod]
        public async Task CanUpdateUserAccountDetailsAsync()
        {
            this._userManagementRepository
               .Setup(m => m.UpdateUserAccountDetailsAsync(this.Users.FirstOrDefault()))
               .ReturnsAsync(true);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            var canUpdateUserAccountDetailsAsync = await this._userManagementService.UpdateUserAccountDetailsAsync(this.Users.FirstOrDefault());

            Assert.IsTrue(canUpdateUserAccountDetailsAsync == true);
        }

        [TestMethod]
        public async Task CanUpdateUserAccountActiveStatusAsync()
        {
            this._userManagementRepository
                  .Setup(m => m.UpdateUserAccountActiveStatusAsync(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<long>()))
                  .ReturnsAsync(true);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            var canUpdateUserAccountActiveStatusAsync = await this._userManagementService.UpdateUserAccountActiveStatusAsync(5, true, 5);

            Assert.IsTrue(canUpdateUserAccountActiveStatusAsync == true);
        }

        [TestMethod]
        public async Task CanUpdateUserAccountLockedStatusAsync()
        {
            this._userManagementRepository
             .Setup(m => m.UpdateUserAccountLockedStatusAsync(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<long>()))
             .ReturnsAsync(true);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            var canUpdateUserAccountLockedStatusAsync = await this._userManagementService.UpdateUserAccountLockedStatusAsync(5, true, 5);

            Assert.IsTrue(canUpdateUserAccountLockedStatusAsync == true);
        }

        [TestMethod]
        public async Task CanChangeUserAccountPasswordAsync()
        {
            this._userManagementRepository
           .Setup(m => m.ChangeUserAccountPasswordAsync(this.Users.FirstOrDefault()))
           .ReturnsAsync(true);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            var canChangeUserAccountPasswordAsync = await this._userManagementService.ChangeUserAccountPasswordAsync(this.Users.FirstOrDefault());

            Assert.IsTrue(canChangeUserAccountPasswordAsync == true);
        }

        #endregion Manage User

        #region User Login History

        [TestMethod]
        public async Task CanGetUserLoginHistoryAsync()
        {
            this._userManagementRepository
         .Setup(m => m.GetUserLoginHistoryAsync(It.IsAny<long>()))
         .ReturnsAsync(this.UserLogins);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            var userLogins = await this._userManagementService.GetUserLoginHistoryAsync(5);

            Assert.IsTrue(userLogins.Count > 1);
        }

        [TestMethod]
        public async Task CanGetUserInCorrectLoginHistoryAsync()
        {
            this._userManagementRepository
     .Setup(m => m.GetUserInCorrectLoginHistoryAsync(It.IsAny<long>()))
     .ReturnsAsync(this.UserLogins);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            var userLogins = await this._userManagementService.GetUserInCorrectLoginHistoryAsync(5);

            Assert.IsTrue(userLogins.Count > 1);
        }

        #endregion User Login History

        #region Manage User Roles

        [TestMethod]
        public async Task CanGetUserRolesAsync()
        {
            this._userManagementRepository
         .Setup(m => m.GetUserRolesAsync(It.IsAny<long>()))
         .ReturnsAsync(this.UserRoles);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            var userRoles = await this._userManagementService.GetUserRolesAsync(5);

            Assert.IsTrue(userRoles.Count > 1);
        }

        [TestMethod]
        public async Task CanEditUserRolesAsync()
        {
            this._userManagementRepository
         .Setup(m => m.EditUserRolesAsync(It.IsAny<List<UserRole>>(), It.IsAny<long>()))
         .ReturnsAsync(true);

            _userManagementService = new UserManagementService(this._appSetting, this._userManagementRepository.Object);

            var canEditUserRolesAsync = await this._userManagementService.EditUserRolesAsync(new List<UserRole>(), 5);

            Assert.IsTrue(canEditUserRolesAsync == true);
        }

        #endregion Manage User Roles

        #endregion Public Methods
    }
}