using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CompName.ManageStocks.CrossCutting.Configuration;
using CompName.ManageStocks.ServiceInterface;
using CompName.ManageStocks.WebAppMVC.Areas.Authentication.Controllers;
using CompName.ManageStocks.WebAppMVC.Infrastructure.AutoMapper;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class AuthControllerTest
    {
        #region Private Variables

        private Mock<IHttpContextAccessor> _httpContextAccessor { get; set; }

        private AppSetting _appSetting { get; set; }

        private Mock<IAuthenticationService> _authenticationService { get; set; }

        private IMapper _mapper { get; set; }

        private AuthController _authController { get; set; }

        #endregion Private Variables

        #region Setup

        [TestInitialize]
        public void Setup()
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

        #endregion Setup

        [TestMethod]
        public void TestMethod1()
        {
        }

        #region Public Test Methods

        #region Register User

        [TestMethod]
        public async Task CanGetRegisterUserViewAsync()
        {
            var result = await this._authController.RegisterUserAsync();

            //Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;
            Assert.IsTrue(viewResult.ViewName == "RegisterUserAsync1");
            Assert.IsTrue(viewResult.Model != null);
        }

        //[TestMethod]
        //public async ValueTask IsUserNameExistsAsync()
        //{
        //    var result = await this._authController.IsUserNameExistsAsync("");

        //    Assert.IsType<ViewResult>(result);
        //    //var jsonResult = (bool)((JsonResult)result).Value;

        //    //Assert.True(jsonResult);
        //}

        #endregion Register User

        #endregion Public Test Methods
    }
}