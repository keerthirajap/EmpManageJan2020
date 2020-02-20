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
using CompName.ManageStocks.WebAppMVC.Areas.Home.Controllers;
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
    public class DashboardControllerTest
    {
        #region Private Variables

        private Mock<IHttpContextAccessor> _httpContextAccessor { get; set; }

        private Mock<AppSetting> _appSetting { get; set; }

        private IMapper _mapper { get; set; }

        private DashboardController _dashboardController { get; set; }

        #endregion Private Variables

        #region Constructor

        public DashboardControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            this._appSetting = new Mock<AppSetting>();
            this._mapper = mockMapper.CreateMapper();

            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();
            context.TraceIdentifier = Guid.NewGuid().ToString();
            _httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

            this._dashboardController = new DashboardController();
        }

        #endregion Constructor

        #region Public Test Methods

        [Fact]
        public async Task CanRunIndex()
        {
            //Act
            var result = await this._dashboardController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.True(viewResult.ViewName == "Index");
        }

        #endregion Public Test Methods
    }
}