using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using AutoMapper;
using CompName.ManageStocks.CrossCutting.Configuration;
using CompName.ManageStocks.Domain.Admin;
using CompName.ManageStocks.Domain.Authentication;
using CompName.ManageStocks.ServiceConcrete;
using CompName.ManageStocks.ServiceInterface;
using CompName.ManageStocks.WebAppMVC.Areas.Admin.Controllers;
using CompName.ManageStocks.WebAppMVC.Areas.Admin.Models.UserManagement;
using CompName.ManageStocks.WebAppMVC.Infrastructure.AutoMapper;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace WebAppMVC.Test
{
    [ExcludeFromCodeCoverage]
    public class UserManagementControllerTest
    {
        private Mock<IUserManagementService> _userManagementService { get; set; }
        private Mock<AppSetting> _appSetting { get; set; }

        private IMapper _mapper { get; set; }

        private UserManagementController _userManagementController { get; set; }

        private List<User> Users { get; set; }
        private List<UserGender> UserGenders { get; set; }
        private List<UserTitle> UserTitles { get; set; }

        public UserManagementControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            this._userManagementService = new Mock<IUserManagementService>(MockBehavior.Strict);
            this._appSetting = new Mock<AppSetting>();
            this._mapper = mockMapper.CreateMapper();

            this.Users = Builder<User>.CreateListOfSize(100).Build().ToList();
            this.UserGenders = Builder<UserGender>.CreateListOfSize(10).Build().ToList();
            this.UserTitles = Builder<UserTitle>.CreateListOfSize(10).Build().ToList();

            this._userManagementService
              .Setup(m => m.GetAllUserAccountsAsync())
              .ReturnsAsync(this.Users);

            this._userManagementService
               .Setup(m => m.GetUserAccountDetailsAsync(It.IsAny<long>()))
               .ReturnsAsync(this.Users.FirstOrDefault());

            this._userManagementService
               .Setup(m => m.GetAllUserGenderDetailsAsync())
               .ReturnsAsync(this.UserGenders);

            this._userManagementService
                .Setup(m => m.GetAllUserTitleDetailsAsync())
                .ReturnsAsync(this.UserTitles);

            this._userManagementService
                .Setup(m => m.UpdateUserAccountDetailsAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            this._userManagementService
                .Setup(m => m.UpdateUserAccountActiveStatusAsync(
                                It.IsAny<long>()
                                , It.IsAny<bool>()
                                 , It.IsAny<long>()
                ))
                .ReturnsAsync(true);

            // Arrange
            this._userManagementController = new UserManagementController(
                                       this._mapper
                                       , this._appSetting.Object
                                       , this._userManagementService.Object
                                        );
        }

        #region Get All Users

        [Fact]
        public async Task CanGetAllUserAccountsViewAsync()
        {
            var result = await this._userManagementController.GetAllUserAccountsViewAsync();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(result != null);
        }

        [Fact]
        public async Task CanGetAllUserAccountsDataAsync()
        {
            var result = await this._userManagementController.GetAllUserAccountsDataAsync();

            var jsonResult = Assert.IsType<JsonResult>(result);

            Assert.True(jsonResult.Value != null);
        }

        #endregion Get All Users

        #region Manage User

        [Fact]
        public async Task CanEditUserAccountDetailsAsync()
        {
            var result = await this._userManagementController.EditUserAccountDetailsAsync(5);

            var viewResult = Assert.IsType<ViewResult>(result);

            var updateUserAccountViewModel = Assert.IsType<UpdateUserAccountViewModel>(viewResult.Model);

            Assert.True(viewResult != null);
            Assert.True(updateUserAccountViewModel != null);
            Assert.True(updateUserAccountViewModel.UserGenders.Count > 1);
            Assert.True(updateUserAccountViewModel.UserTitles.Count > 1);
        }

        [Fact]
        public async Task CanUpdateUserAccountDetailsAsync()
        {
            UpdateUserAccountViewModel updateUserAccountViewModel = new UpdateUserAccountViewModel();
            var result = await this._userManagementController.UpdateUserAccountDetailsAsync(updateUserAccountViewModel);

            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value.ToString();
            var jsonResultDeserializeObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            var jsonResultStatus = jsonResultDeserializeObject.Status.Value;
            Assert.Equal(jsonResultStatus, "Success");
        }

        [Fact]
        public async Task CanUpdateUserAccountActiveStatus()
        {
            var result = await this._userManagementController.UpdateUserAccountActiveStatusAsync(5, true);

            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value.ToString();
            var jsonResultDeserializeObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            var jsonResultStatus = jsonResultDeserializeObject.Status.Value;

            Assert.Equal(jsonResultStatus, "Success");
        }

        #endregion Manage User
    }
}