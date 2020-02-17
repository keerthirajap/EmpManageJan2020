using System.Collections.Generic;
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

namespace ServiceTest
{
    [TestClass]
    public class UserManagementServiceTest
    {
        private Mock<IUserManagementRepository> _userManagementRepository { get; set; }
        private Mock<AppSetting> _appSetting { get; set; }
        private UserManagementService _userManagementService { get; set; }

        private List<User> Users { get; set; }

        [TestInitialize]
        public void Setup()
        {
            this._userManagementRepository = new Mock<IUserManagementRepository>();
            this._appSetting = new Mock<AppSetting>();

            this.Users = Builder<User>.CreateListOfSize(100).Build().ToList();
            this._userManagementRepository.Setup(m => m.GetAllUserAccountsAsync()).ReturnsAsync(this.Users);

            List<UserGender> userGenders = Builder<UserGender>.CreateListOfSize(10).Build().ToList();
            this._userManagementRepository.Setup(m => m.GetAllUserGenderDetailsAsync()).ReturnsAsync(userGenders);

            _userManagementService = new UserManagementService(this._appSetting.Object, this._userManagementRepository.Object);
        }

        [TestMethod]
        public async Task CanGetAllUserAccountsAsync()
        {
            List<User> users = await this._userManagementService.GetAllUserAccountsAsync();

            Assert.IsTrue(users.Count >= 1);
        }

        [TestMethod]
        public async Task CanGetAllUserGenderDetailsAsync()
        {
            List<UserGender> userGenders = await this._userManagementService.GetAllUserGenderDetailsAsync();
            Assert.IsTrue(userGenders.Count >= 1);
        }
    }
}