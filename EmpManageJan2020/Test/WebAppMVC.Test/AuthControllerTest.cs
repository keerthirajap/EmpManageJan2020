using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
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
using CompName.ManageStocks.WebAppMVC.Areas.Authentication.Controllers;
using CompName.ManageStocks.WebAppMVC.Areas.Authentication.Models.Auth;
using CompName.ManageStocks.WebAppMVC.Controllers;
using CompName.ManageStocks.WebAppMVC.Infrastructure.AutoMapper;
using CompName.ManageStocks.WebAppMVC.Models;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace WebAppMVC.Test
{
    [ExcludeFromCodeCoverage]
    public class AuthControllerTest
    {
        #region Private Variables

        private Mock<IHttpContextAccessor> _httpContextAccessor { get; set; }

        private AppSetting _appSetting { get; set; }

        private Mock<CompName.ManageStocks.ServiceInterface.IAuthenticationService> _authenticationService { get; set; }

        private IMapper _mapper { get; set; }

        private AuthController _authController { get; set; }

        private List<User> Users { get; set; }
        private UserLogin UserLogin { get; set; }
        private List<UserRole> UserRoles { get; set; }

        #endregion Private Variables

        #region Constructor

        public AuthControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            this._appSetting = Builder<AppSetting>.CreateListOfSize(1).Build().ToList().FirstOrDefault();
            this._mapper = mockMapper.CreateMapper();

            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();
            context.TraceIdentifier = Guid.NewGuid().ToString();
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            context.Connection.RemoteIpAddress = ipAddress;

            _httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

            this.Users = Builder<User>.CreateListOfSize(100).Build().ToList();
            this.UserLogin = Builder<UserLogin>.CreateListOfSize(1)
                            .Build().ToList().FirstOrDefault();

            this.UserRoles = Builder<UserRole>.CreateListOfSize(5).Build().ToList();

            this._authenticationService = new Mock<CompName.ManageStocks.ServiceInterface.IAuthenticationService>(MockBehavior.Strict);

            this._authenticationService
            .Setup(m => m.RegisterUserAsync(It.IsAny<User>()))
            .ReturnsAsync(this.Users.FirstOrDefault());

            this._authenticationService
            .Setup(m => m.ValidateUserLoginAsync(It.IsAny<UserLogin>()))
            .ReturnsAsync((this.UserLogin, this.UserRoles));

            this._authenticationService
             .Setup(m => m.IsUserNameExistsAsync(It.IsAny<string>()))
             .ReturnsAsync(true);

            this._authenticationService
             .Setup(m => m.IsEmailIdExistsAsync(It.IsAny<string>()))
             .ReturnsAsync(true);

            this._authController = new AuthController(
                 this._mapper
                 , this._httpContextAccessor.Object
                , this._appSetting
                , this._authenticationService.Object);
        }

        #endregion Constructor

        #region Public Test Methods

        #region Register User

        [Fact]
        public async Task CanGetRegisterUserViewAsync()
        {
            var result = await this._authController.RegisterUserAsync();

            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.True(viewResult.ViewName == "RegisterUser");
            Assert.IsType<RegisterUserViewModel>(viewResult.Model);
            Assert.True(viewResult.Model != null);
        }

        [Fact]
        public async Task CanRegisterUserAsync()
        {
            var result = await this._authController.RegisterUserAsync(new RegisterUserViewModel());

            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value.ToString();
            var jsonResultDeserializeObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            var jsonResultStatus = jsonResultDeserializeObject.Status.Value;
            Assert.Equal(jsonResultStatus, "Success");
        }

        [Fact]
        public async Task IsUserNameExistsAsync()
        {
            var result = await this._authController.IsUserNameExistsAsync("");

            Assert.IsType<JsonResult>(result);

            var jsonResult = (bool)((JsonResult)result).Value;

            Assert.True(jsonResult == false);
        }

        [Fact]
        public async Task IsEmailIdExistsAsync()
        {
            var result = await this._authController.IsEmailIdExistsAsync("");

            Assert.IsType<JsonResult>(result);

            var jsonResult = (bool)((JsonResult)result).Value;

            Assert.True(jsonResult == false);
        }

        #endregion Register User

        #region User Login

        [Fact]
        public async Task CanGetLoginViewAsync()
        {
            var result = await this._authController.LoginAsync();

            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.True(viewResult.ViewName == "Login");
            Assert.IsType<LoginViewModel>(viewResult.Model);
            Assert.True(viewResult.Model != null);
        }

        [Fact]
        public async Task CanLoginAsync()
        {
            var result = await this._authController.LoginAsync(new LoginViewModel());

            Assert.IsType<JsonResult>(result);

            var jsonResult = ((JsonResult)result).Value.ToString();
            var jsonResultDeserializeObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            var jsonResultStatus = jsonResultDeserializeObject.Status.Value;
            Assert.Equal(jsonResultStatus, "Warning");
        }

        #endregion User Login

        [Fact]
        public async Task CanLogOutAsync()
        {
            var result = await this._authController.LogOutAsync();

            Assert.IsType<Exception>(result);
        }

        #endregion Public Test Methods
    }
}