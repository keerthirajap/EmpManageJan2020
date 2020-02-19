namespace WebAppMVC.Test.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using CompName.ManageStocks.Domain.Authentication;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.AutoMapper;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.CustomFilters;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.Extensions;
    using FizzWare.NBuilder;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class WebAppMVCExtensionsTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void CanGetLoggedInUserDetails()
        {
            UserAuthentication userAuthenticationModel = new UserAuthentication();
            userAuthenticationModel = Builder<UserAuthentication>.CreateListOfSize(1).Build().ToList().FirstOrDefault();
            string userData = JsonConvert.SerializeObject(userAuthenticationModel);

            List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Name, userAuthenticationModel.UserName),
                                        new Claim(ClaimTypes.NameIdentifier, userAuthenticationModel.UserId.ToString()),
                                        new Claim(ClaimTypes.Authentication, "Form"),
                                        new Claim("UserId",  userAuthenticationModel.UserId.ToString()),
                                        new Claim("AuthenticationGUID",  userAuthenticationModel.AuthenticationGUID),
                                        new Claim("LoggedOn", userAuthenticationModel.LoggedOn.ToString()),
                                        new Claim("AuthenticationExpiresOn", "AuthenticationExpiresOn",  userAuthenticationModel.AuthenticationExpiresOn.ToString()),
                                        new Claim(ClaimTypes.UserData, userData),
                                    };

            var identityClaims = new ClaimsIdentity(claims);

            ClaimsPrincipal principal = new ClaimsPrincipal(identityClaims);

            var userAuthentication = WebAppMVCExtensions.GetLoggedInUserDetails(principal);

            Assert.AreEqual(userAuthenticationModel.UserName, userAuthentication.UserName);
        }
    }
}