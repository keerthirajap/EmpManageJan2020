using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
using CompName.ManageStocks.WebAppMVC.Controllers;
using CompName.ManageStocks.WebAppMVC.Infrastructure.AutoMapper;
using CompName.ManageStocks.WebAppMVC.Models;
using FizzWare.NBuilder;
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

        private Mock<IAuthenticationService> _authenticationService { get; set; }

        private IMapper _mapper { get; set; }

        private AuthController _authController { get; set; }

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
            _httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

            this._authenticationService = new Mock<IAuthenticationService>(MockBehavior.Strict);

            this._authenticationService
             .Setup(m => m.IsUserNameExistsAsync(It.IsAny<string>()))
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
            Assert.True(viewResult.Model != null);
        }

        [Fact]
        public async Task IsUserNameExistsAsync()
        {
            var result = await this._authController.IsUserNameExistsAsync("");

            Assert.IsType<JsonResult>(result);

            var jsonResult = (bool)((JsonResult)result).Value;

            Assert.True(jsonResult == false);
        }

        #endregion Register User

        #endregion Public Test Methods
    }
}