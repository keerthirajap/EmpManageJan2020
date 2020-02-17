using System;
using System.Collections.Generic;
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
    [TestClass]
    public class UserManagementRepository
    {
        private Mock<IUserManagementRepository> _userManagementRepository { get; set; }

        private List<User> Users { get; set; }

        [TestInitialize]
        public void Setup()
        {
            this._userManagementRepository = new Mock<IUserManagementRepository>();
            this.Users = Builder<User>.CreateListOfSize(100).Build().ToList();
            this._userManagementRepository.Setup(m => m.GetAllUserAccountsAsync()).ReturnsAsync(this.Users);

            List<UserGender> userGenders = Builder<UserGender>.CreateListOfSize(10).Build().ToList();
            this._userManagementRepository.Setup(m => m.GetAllUserGenderDetailsAsync()).ReturnsAsync(userGenders);

            List<UserTitle> userTitles = Builder<UserTitle>.CreateListOfSize(5).Build().ToList();
            this._userManagementRepository.Setup(m => m.GetAllUserTitleDetailsAsync()).ReturnsAsync(userTitles);

            this._userManagementRepository.Setup(m => m.UpdateUserAccountDetailsAsync(It.IsAny<User>())).ReturnsAsync(true);

            this._userManagementRepository.Setup(m => m.UpdateUserAccountActiveStatusAsync(
                                                            It.IsAny<long>()
                                                            , It.IsAny<bool>()
                                                             , It.IsAny<long>()
                                                        )).ReturnsAsync(true);
        }

        [TestMethod]
        public async Task CanGetAllUserAccountsAsync()
        {
            List<User> users = await this._userManagementRepository.Object.GetAllUserAccountsAsync();
            Assert.IsTrue(users.Count >= 1);
        }

        [TestMethod]
        public async Task CanGetAllUserGenderDetailsAsync()
        {
            List<UserGender> userGenders = await this._userManagementRepository.Object.GetAllUserGenderDetailsAsync();
            Assert.IsTrue(userGenders.Count >= 1);
        }

        [TestMethod]
        public async Task CanGetAllUserTitleDetailsAsync()
        {
            List<UserTitle> userTitles = await this._userManagementRepository.Object.GetAllUserTitleDetailsAsync();
            Assert.IsTrue(userTitles.Count >= 1);
        }

        [TestMethod]
        public async Task CanUpdateUserAccountDetailsAsync()
        {
            bool canUpdateUserAccountDetailsAsync = await this._userManagementRepository.Object.UpdateUserAccountDetailsAsync(this.Users.FirstOrDefault());
            Assert.AreEqual(canUpdateUserAccountDetailsAsync, true); // Verify it has the expected productid
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
            Assert.AreEqual(canUpdateUserAccountActiveStatusAsync, true); // Verify it has the expected productid
        }
    }
}