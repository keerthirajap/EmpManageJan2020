using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using CompName.ManageStocks.Domain.Admin;
using CompName.ManageStocks.Domain.Authentication;
using CompName.ManageStocks.RepositoryInterface;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Repository.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UserManagementRepository
    {
        private Mock<IUserManagementRepository> _userManagementRepository { get; set; }

        private List<User> Users { get; set; }

        private List<UserLogin> UserLogins { get; set; }

        private List<UserRole> UserRoles { get; set; }

        [TestInitialize]
        public void Setup()
        {
            this._userManagementRepository = new Mock<IUserManagementRepository>();
            this.Users = Builder<User>.CreateListOfSize(100).Build().ToList();
            this.UserLogins = Builder<UserLogin>.CreateListOfSize(100).Build().ToList();
            this.UserRoles = Builder<UserRole>.CreateListOfSize(100).Build().ToList();

            this._userManagementRepository.Setup(m => m.UpdateUserAccountActiveStatusAsync(
                                                          It.IsAny<long>()
                                                          , It.IsAny<bool>()
                                                           , It.IsAny<long>()
                                                      )).ReturnsAsync(true);

            this._userManagementRepository.Setup(m => m.UpdateUserAccountLockedStatusAsync(
                                                It.IsAny<long>()
                                                , It.IsAny<bool>()
                                                 , It.IsAny<long>()
                                            )).ReturnsAsync(true);
        }

        [TestMethod]
        public async Task CanGetAllUserAccountsAsync()
        {
            this._userManagementRepository.Setup(m => m.GetAllUserAccountsAsync()).ReturnsAsync(this.Users);

            List<User> users = await this._userManagementRepository.Object.GetAllUserAccountsAsync();
            Assert.IsTrue(users.Count >= 1);
        }

        [TestMethod]
        public async Task CanGetAllUserGenderDetailsAsync()
        {
            List<UserGender> userGenders = Builder<UserGender>.CreateListOfSize(10).Build().ToList();
            this._userManagementRepository.Setup(m => m.GetAllUserGenderDetailsAsync()).ReturnsAsync(userGenders);

            List<UserGender> userGendersResult = await this._userManagementRepository.Object.GetAllUserGenderDetailsAsync();
            Assert.IsTrue(userGendersResult.Count >= 1);
        }

        [TestMethod]
        public async Task CanGetAllUserTitleDetailsAsync()
        {
            List<UserTitle> userTitles = Builder<UserTitle>.CreateListOfSize(5).Build().ToList();
            this._userManagementRepository.Setup(m => m.GetAllUserTitleDetailsAsync()).ReturnsAsync(userTitles);

            List<UserTitle> userTitlesResult = await this._userManagementRepository.Object.GetAllUserTitleDetailsAsync();
            Assert.IsTrue(userTitlesResult.Count >= 1);
        }

        #region Manage User

        [TestMethod]
        public async Task CanGetUserAccountDetailsAsync()
        {
            this._userManagementRepository.Setup(m => m.GetUserAccountDetailsAsync(It.IsAny<long>())).ReturnsAsync(this.Users.FirstOrDefault());

            User user = await this._userManagementRepository.Object.GetUserAccountDetailsAsync(this.Users.FirstOrDefault().UserId);
            Assert.IsTrue(user != null);
        }

        [TestMethod]
        public async Task CanUpdateUserAccountDetailsAsync()
        {
            this._userManagementRepository.Setup(m => m.UpdateUserAccountDetailsAsync(It.IsAny<User>())).ReturnsAsync(true);

            bool canUpdateUserAccountDetailsAsync = await this._userManagementRepository.Object.UpdateUserAccountDetailsAsync(this.Users.FirstOrDefault());
            Assert.AreEqual(canUpdateUserAccountDetailsAsync, true);
        }

        [TestMethod]
        public async Task CanUpdateUserAccountLockedStatusAsync()
        {
            bool canUpdateUserAccountLockedStatusAsync = await this
                                                       ._userManagementRepository
                                                       .Object
                                                       .UpdateUserAccountLockedStatusAsync(
                                                           this.Users.FirstOrDefault().UserId
                                                           , this.Users.FirstOrDefault().IsActive
                                                           , this.Users.FirstOrDefault().ModifiedBy
                                                           );
            Assert.AreEqual(canUpdateUserAccountLockedStatusAsync, true);
        }

        [TestMethod]
        public async Task CanUpdateUserAccountActiveStatusAsync()
        {
            bool canUpdateUserAccountActiveStatusAsync = await this
                                                        ._userManagementRepository
                                                        .Object
                                                        .UpdateUserAccountActiveStatusAsync(
                                                            this.Users.FirstOrDefault().UserId
                                                            , this.Users.FirstOrDefault().IsActive
                                                            , this.Users.FirstOrDefault().ModifiedBy
                                                            );
            Assert.AreEqual(canUpdateUserAccountActiveStatusAsync, true);
        }

        [TestMethod]
        public async Task CanChangeUserAccountPasswordAsync()
        {
            this._userManagementRepository.Setup(m => m.ChangeUserAccountPasswordAsync(It.IsAny<User>())).ReturnsAsync(true);
            bool canChangeUserAccountPasswordAsync = await this._userManagementRepository.Object.ChangeUserAccountPasswordAsync(this.Users.FirstOrDefault());
            Assert.AreEqual(canChangeUserAccountPasswordAsync, true);
        }

        #endregion Manage User

        #region User Login History

        [TestMethod]
        public async Task CanGetUserLoginHistoryAsync()
        {
            this._userManagementRepository.Setup(m => m.GetUserLoginHistoryAsync(It.IsAny<long>())).ReturnsAsync(this.UserLogins);
            var userLogins = await this._userManagementRepository.Object.GetUserLoginHistoryAsync(5);
            Assert.IsTrue(userLogins.Count >= 1);
        }

        [TestMethod]
        public async Task CanGetUserInCorrectLoginHistoryAsync()
        {
            this._userManagementRepository.Setup(m => m.GetUserInCorrectLoginHistoryAsync(It.IsAny<long>())).ReturnsAsync(this.UserLogins);
            var userLogins = await this._userManagementRepository.Object.GetUserInCorrectLoginHistoryAsync(5);
            Assert.IsTrue(userLogins.Count >= 1);
        }

        #endregion User Login History

        #region Manage User Roles

        [TestMethod]
        public async Task CanGetUserRolesAsync()
        {
            this._userManagementRepository.Setup(m => m.GetUserRolesAsync(It.IsAny<long>())).ReturnsAsync(this.UserRoles);
            var userRoles = await this._userManagementRepository.Object.GetUserRolesAsync(5);
            Assert.IsTrue(userRoles.Count >= 1);
        }

        [TestMethod]
        public async Task CanEditUserRolesAsync()
        {
            this._userManagementRepository.Setup(m => m.EditUserRolesAsync(It.IsAny<List<UserRole>>(), It.IsAny<long>())).ReturnsAsync(true);
            var canEditUserRolesAsync = await this._userManagementRepository.Object.EditUserRolesAsync(this.UserRoles, 5);

            Assert.AreEqual(canEditUserRolesAsync, true);
        }

        #endregion Manage User Roles
    }
}