using System;
using System.Threading.Tasks;
using AutoMapper;
using CompName.ManageStocks.CrossCutting.Configuration;
using CompName.ManageStocks.ServiceConcrete;
using CompName.ManageStocks.ServiceInterface;
using CompName.ManageStocks.WebAppMVC.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace WebAppMVCTest
{
    public class UserManagementControllerTest
    {
        private Mock<IUserManagementService> _userManagementService { get; set; }
        private Mock<AppSetting> _appSetting { get; set; }

        private Mock<IMapper> _mapper { get; set; }

        private UserManagementController _userManagementController { get; set; }

        public UserManagementControllerTest()
        {
            this._userManagementService = new Mock<IUserManagementService>();
            this._appSetting = new Mock<AppSetting>();
            this._mapper = new Mock<IMapper>();

            // Arrange
            this._userManagementController = new UserManagementController(
                                       this._mapper.Object
                                       , this._appSetting.Object
                                       , this._userManagementService.Object
                                        );
        }

        [Fact]
        public async Task CanGetUserAccountsAsync()
        {
            //Act
            var result = await this._userManagementController.GetUserAccountsAsync();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}