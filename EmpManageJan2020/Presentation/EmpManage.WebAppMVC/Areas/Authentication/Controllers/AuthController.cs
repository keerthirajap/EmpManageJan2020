namespace EmpManage.WebAppMVC.Areas.Authentication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using EmpManage.CrossCutting.Configuration;
    using EmpManage.Domain;
    using EmpManage.Domain.Authentication;
    using EmpManage.ServiceInterface;
    using EmpManage.WebAppMVC.Areas.Authentication.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    [AutoValidateAntiforgeryToken]
    [Area("Authentication")]
    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private readonly AppSetting _appSetting;

        private readonly ServiceInterface.IAuthenticationService _authenticationService;

        public AuthController(
                               IMapper mapper,
                               IHttpContextAccessor httpContextAccessor,
                               AppSetting appSetting,
                               ServiceInterface.IAuthenticationService authenticationService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
            this._appSetting = appSetting;
            this._authenticationService = authenticationService;
        }

        [Route("RegisterUser")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> RegisterUser()
        {
            RegisterUserViewModel registerUserViewModel = new RegisterUserViewModel();
            return await Task.Run(() => this.View(registerUserViewModel));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserViewModel registerUserViewModel)
        {
            dynamic ajaxReturn = new JObject();
            var user = this._mapper.Map<User>(registerUserViewModel);

            user = await this._authenticationService.RegisterUserAsync(user);

            if (user.UserId > 0)
            {
                ajaxReturn.Status = "Success";
                ajaxReturn.Message = registerUserViewModel.UserName + " - user sucessfully created. Redirecting to home page.";
                ajaxReturn.UserId = user.UserId;
                ajaxReturn.UserName = registerUserViewModel.UserName;
                ajaxReturn.Title = "Congratulations";

                await this.AuthneticateUserWithCookies(user);
            }

            return this.Json(ajaxReturn);
        }

        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return await Task.Run(() => this.View(loginViewModel));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            dynamic ajaxReturn = new JObject();
            UserLogin userLogin = new UserLogin();
            userLogin.Password = loginViewModel.Password;
            userLogin.UserName = loginViewModel.UserName;

            userLogin = await this._authenticationService.ValidateUserLoginAsync(userLogin);

            if (userLogin.IsUserAuthenticated)
            {
            }

            return this.Json(ajaxReturn);
        }

        private async Task AuthneticateUserWithCookies(User user)
        {
            UserAuthentication userAuthenticationModel = new UserAuthentication();
            userAuthenticationModel.UserName = user.UserName;
            userAuthenticationModel.UserId = user.UserId;
            userAuthenticationModel.LoggedOn = DateTime.Now;
            userAuthenticationModel.AuthenticationExpiresOn = DateTime.Now.AddHours(1);
            userAuthenticationModel.AuthenticationGUID = new Guid().ToString();

            string userData = JsonConvert.SerializeObject(userAuthenticationModel);

            var identity = (ClaimsIdentity)this.HttpContext.User.Identity;

            List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.NameIdentifier, user.UserName),
                                        new Claim(ClaimTypes.Authentication, "Authenticated"),
                                        new Claim("http://example.org/claims/AuthenticationGUID", "AuthenticationGUID",  userAuthenticationModel.AuthenticationGUID),
                                        new Claim("http://example.org/claims/LoggedOn", "LoggedOn",  userAuthenticationModel.LoggedOn.ToString()),
                                        new Claim("http://example.org/claims/AuthenticationExpiresOn", "AuthenticationExpiresOn",  userAuthenticationModel.AuthenticationExpiresOn.ToString()),
                                        new Claim(ClaimTypes.UserData, userData),
                                    };
            var identityClaims = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identityClaims);
            await this.HttpContext.SignInAsync(
                                        CookieAuthenticationDefaults.AuthenticationScheme,
                                        principal,
                                        new AuthenticationProperties
                                        {
                                            ExpiresUtc = userAuthenticationModel.AuthenticationExpiresOn,
                                            IsPersistent = true,
                                        });
        }
    }
}