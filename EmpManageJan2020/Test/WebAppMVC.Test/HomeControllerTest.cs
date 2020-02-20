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
    public class HomeControllerTest
    {
        #region Private Variables

        private Mock<ILogger<HomeController>> _logger;

        private Mock<IHttpContextAccessor> _httpContextAccessor { get; set; }

        private DefaultHttpContext _defaultHttpContext { get; set; }

        private Mock<AppSetting> _appSetting { get; set; }

        private IMapper _mapper { get; set; }

        private HomeController _homeController { get; set; }

        #endregion Private Variables

        #region Constructor

        public HomeControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            this._appSetting = new Mock<AppSetting>();
            this._mapper = mockMapper.CreateMapper();

            _logger = new Mock<ILogger<HomeController>>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        #endregion Constructor

        #region Public Test Methods

        [Fact]
        public async Task CanRunIndex()
        {
            //Arrange
            this._homeController = new HomeController(_logger.Object, _httpContextAccessor.Object);

            //Act
            var result = await this._homeController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.True(viewResult.ViewName == "Index");
        }

        [Fact]
        public async Task CanRunError()
        {
            //Arrange
            _defaultHttpContext = new DefaultHttpContext();
            _defaultHttpContext.TraceIdentifier = Guid.NewGuid().ToString();
            _httpContextAccessor.Setup(_ => _.HttpContext).Returns(_defaultHttpContext);

            this._homeController = new HomeController(_logger.Object, _httpContextAccessor.Object);

            //Act
            var result = await this._homeController.Error();

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.True(viewResult.ViewName == "Error");
            Assert.IsType<ErrorViewModel>(viewResult.Model);
            Assert.True(viewResult.Model != null);
        }

        [Fact]
        public async Task CanRunAccessDenied()
        {
            //Arrange
            this._homeController = new HomeController(_logger.Object, _httpContextAccessor.Object);

            //Act
            var result = await this._homeController.AccessDenied();

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.True(viewResult.ViewName == "AccessDenied");
        }

        #endregion Public Test Methods
    }
}