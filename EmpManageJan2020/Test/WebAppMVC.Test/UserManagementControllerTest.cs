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
using CompName.ManageStocks.WebAppMVC.Areas.Authentication.Models.Auth;
using CompName.ManageStocks.WebAppMVC.Areas.Manufacturer.Controllers;
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

        private List<UserLogin> UserLogins { get; set; }

        private List<UserRole> UserRoles { get; set; }

        public UserManagementControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            this._userManagementService = new Mock<IUserManagementService>(MockBehavior.Loose);
            this._appSetting = new Mock<AppSetting>();
            this._mapper = mockMapper.CreateMapper();

            this.Users = Builder<User>.CreateListOfSize(100).Build().ToList();
            this.UserGenders = Builder<UserGender>.CreateListOfSize(10).Build().ToList();
            this.UserTitles = Builder<UserTitle>.CreateListOfSize(10).Build().ToList();
            this.UserLogins = Builder<UserLogin>.CreateListOfSize(10).Build().ToList();
            this.UserRoles = Builder<UserRole>.CreateListOfSize(10).Build().ToList();
        }

        #region Public Test Methods

        #region Get All Users

        [Fact]
        public async Task CanGetAllUserAccountsViewAsync()
        {
            //Arrange
            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );
            //Act
            var result = await this._userManagementController.GetAllUserAccountsViewAsync();

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.True(viewResult.ViewName == "GetAllUserAccounts", "View name is incorrect");
        }

        [Fact]
        public async Task CanGetAllUserAccountsDataAsync()
        {
            //Arrange
            this._userManagementService
                  .Setup(m => m.GetAllUserAccountsAsync())
                  .ReturnsAsync(this.Users);

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            //Act
            var result = await this._userManagementController.GetAllUserAccountsDataAsync();

            //Assert
            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value;
            Assert.True(jsonResult != null);
        }

        #endregion Get All Users

        #region Manage User

        [Fact]
        public async Task CanEditUserAccountDetailsAsync()
        {
            //Arrange
            this._userManagementService
              .Setup(m => m.GetUserAccountDetailsAsync(It.IsAny<long>()))
              .ReturnsAsync(this.Users.FirstOrDefault());

            this._userManagementService
               .Setup(m => m.GetAllUserGenderDetailsAsync())
               .ReturnsAsync(this.UserGenders);

            this._userManagementService
                .Setup(m => m.GetAllUserTitleDetailsAsync())
                .ReturnsAsync(this.UserTitles);

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            //Act
            var result = await this._userManagementController.EditUserAccountDetailsAsync(5);

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;

            var updateUserAccountViewModel = Assert.IsType<UpdateUserAccountViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewName == "EditUserAccountDetails", "View name is incorrect");
            Assert.True(updateUserAccountViewModel != null);
            Assert.True(updateUserAccountViewModel.UserGenders.Count > 1);
            Assert.True(updateUserAccountViewModel.UserTitles.Count > 1);
        }

        [Fact]
        public async Task CanUpdateUserAccountDetailsAsync()
        {
            //Arrange
            this._userManagementService
            .Setup(m => m.UpdateUserAccountDetailsAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            UpdateUserAccountViewModel updateUserAccountViewModel = new UpdateUserAccountViewModel();

            //Act
            var result = await this._userManagementController.UpdateUserAccountDetailsAsync(updateUserAccountViewModel);

            //Assert
            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value.ToString();
            var jsonResultDeserializeObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            var jsonResultStatus = jsonResultDeserializeObject.Status.Value;
            Assert.Equal(jsonResultStatus, "Success");
        }

        [Fact]
        public async Task CanUpdateUserAccountActiveStatus()
        {
            //Arrange
            this._userManagementService
                .Setup(m => m.UpdateUserAccountActiveStatusAsync(It.IsAny<long>(), true, It.IsAny<long>()))
                .ReturnsAsync(true);

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            //Act
            var result = await this._userManagementController.UpdateUserAccountActiveStatusAsync(5, true);

            //Assert
            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value.ToString();
            var jsonResultDeserializeObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            var jsonResultStatus = jsonResultDeserializeObject.Status.Value;

            Assert.Equal(jsonResultStatus, "Success");
        }

        [Fact]
        public async Task CanUpdateUserAccountLockedStatusAsync()
        {
            //Arrange
            this._userManagementService
               .Setup(m => m.UpdateUserAccountLockedStatusAsync(
                               It.IsAny<long>()
                               , It.IsAny<bool>()
                                , It.IsAny<long>()
               ))
               .ReturnsAsync(true);

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            //Act
            var result = await this._userManagementController.UpdateUserAccountLockedStatusAsync(5, true);

            //Assert
            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value.ToString();
            var jsonResultDeserializeObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            var jsonResultStatus = jsonResultDeserializeObject.Status.Value;

            Assert.Equal(jsonResultStatus, "Success");
        }

        [Fact]
        public async Task CanLoadChangePasswordPartialViewAsync()
        {
            //Arrange
            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            //Act
            var result = await this._userManagementController.LoadChangePasswordPartialViewAsync(5, "");

            //Assert
            Assert.IsType<PartialViewResult>(result);

            var viewResult = (PartialViewResult)result;
            Assert.True(viewResult.ViewName == "_ChangeUserAccountPassword", "PartialView name is incorrect");
            Assert.IsType<ChangeUserAccountPasswordViewModel>(viewResult.Model);
            Assert.True(viewResult.Model != null);
        }

        [Fact]
        public async Task CanChangeUserAccountPasswordAsync()
        {
            //Arrange
            this._userManagementService
             .Setup(m => m.ChangeUserAccountPasswordAsync(It.IsAny<User>()))
             .ReturnsAsync(true);

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            //Act
            var result = await this._userManagementController.ChangeUserAccountPasswordAsync(new ChangeUserAccountPasswordViewModel());

            //Assert
            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value.ToString();
            var jsonResultDeserializeObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            var jsonResultStatus = jsonResultDeserializeObject.Status.Value;

            Assert.Equal(jsonResultStatus, "Success");
        }

        #endregion Manage User

        #region User Login History

        [Fact]
        public async Task CanGetUserLoginHistoryViewAsync()
        {
            //Arrange
            this._userManagementService
                 .Setup(m => m.GetUserLoginHistoryAsync(It.IsAny<long>()))
                 .ReturnsAsync(this.UserLogins);
            this._userManagementService
              .Setup(m => m.GetUserAccountDetailsAsync(It.IsAny<long>()))
              .ReturnsAsync(this.Users.FirstOrDefault());

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            //Act
            var result = await this._userManagementController.GetUserLoginHistoryViewAsync(5);

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;

            var results = Assert.IsType<(UserAccountViewModel, List<UserLoginViewModel>)>(viewResult.Model);
            Assert.True(viewResult.ViewName == "UserLoginHistory", "View name is incorrect");

            Assert.True(results.Item1 != null);
            Assert.True(results.Item2.Count > 1);
        }

        [Fact]
        public async Task CanGetUserInCorrectLoginHistoryViewAsync()
        {
            //Arrange
            this._userManagementService
              .Setup(m => m.GetUserInCorrectLoginHistoryAsync(It.IsAny<long>()))
              .ReturnsAsync(this.UserLogins);
            this._userManagementService
              .Setup(m => m.GetUserAccountDetailsAsync(It.IsAny<long>()))
              .ReturnsAsync(this.Users.FirstOrDefault());

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );
            //Act
            var result = await this._userManagementController.GetUserInCorrectLoginHistoryViewAsync(5);

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;

            var results = Assert.IsType<(UserAccountViewModel, List<UserLoginViewModel>)>(viewResult.Model);
            Assert.True(viewResult.ViewName == "UserInCorrectLoginHistory", "View name is incorrect");

            Assert.True(results.Item1 != null);
            Assert.True(results.Item2.Count > 1);
        }

        #endregion User Login History

        #region Manage User Roles

        [Fact]
        public async Task CanGetEditUserRolesViewAsync()
        {
            //Arrange
            this._userManagementService
            .Setup(m => m.GetUserRolesAsync(It.IsAny<long>()))
            .ReturnsAsync(this.UserRoles);

            this._userManagementService
            .Setup(m => m.GetUserAccountDetailsAsync(It.IsAny<long>()))
            .ReturnsAsync(this.Users.FirstOrDefault());

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            //Act
            var result = await this._userManagementController.GetEditUserRolesViewAsync(5);

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;

            var results = Assert.IsType<(UserAccountViewModel, List<UserRoleViewModel>)>(viewResult.Model);
            Assert.True(viewResult.ViewName == "EditUserRoles", "View name is incorrect");

            Assert.True(results.Item1 != null);
            Assert.True(results.Item2.Count > 1);
        }

        [Fact]
        public async Task CanEditUserRolesAsync()
        {
            //Arrange
            this._userManagementService
                  .Setup(m => m.EditUserRolesAsync(this.UserRoles, It.IsAny<long>()))
                  .ReturnsAsync(true);

            this._userManagementController = new UserManagementController(
                                     this._mapper
                                     , this._appSetting.Object
                                     , this._userManagementService.Object
                                      );

            //Act
            var result = await this._userManagementController.EditUserRolesAsync(new List<UserRoleViewModel>());

            //Assert
            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value.ToString();
            var jsonResultDeserializeObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            var jsonResultStatus = jsonResultDeserializeObject.Status.Value;

            Assert.Equal(jsonResultStatus, "Success");
        }

        #endregion Manage User Roles

        #endregion Public Test Methods
    }
}