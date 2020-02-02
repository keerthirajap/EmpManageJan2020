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
            this._httpContextAccessor.HttpContext.Response.Cookies.Delete("EmployeeManage.AuthCookie");

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
                ajaxReturn.Title = "Congratulations";
                ajaxReturn.UserId = user.UserId;
                ajaxReturn.UserName = registerUserViewModel.UserName;

                UserLogin userLogin = new UserLogin();
                userLogin.UserName = registerUserViewModel.UserName;
                userLogin.Password = registerUserViewModel.Password;
                userLogin.LoggingIpAddress = this._httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                userLogin.LoggingBrowser = this._httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
                userLogin.CreatedOn = DateTime.Now;
                userLogin = await this._authenticationService.ValidateUserLoginAsync(userLogin);

                if (userLogin.IsUserAuthenticated)
                {
                    await this.AuthenticateUserWithCookie(userLogin);
                }
            }

            return this.Json(ajaxReturn);
        }

        public async Task<IActionResult> IsUserNameExists(string userName)
        {
            User user = new User();
            bool isUserNameExists = false;

            isUserNameExists = await this._authenticationService.IsUserNameExists(userName);

            if (isUserNameExists)
            {
                return this.Json(false);
            }
            else
            {
                return this.Json(true);
            }
        }

        public async Task<IActionResult> IsEmailIdExists(string emailId)
        {
            User user = new User();
            bool isEmailIdExists = false;

            isEmailIdExists = await this._authenticationService.IsEmailIdExists(emailId);

            if (isEmailIdExists)
            {
                return this.Json(false);
            }
            else
            {
                return this.Json(true);
            }
        }

        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            this._httpContextAccessor.HttpContext.Response.Cookies.Delete("EmployeeManage.AuthCookie");

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

            userLogin = this._mapper.Map<UserLogin>(loginViewModel);

            userLogin.LoggingIpAddress = this._httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            userLogin.LoggingBrowser = this._httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            userLogin.CreatedOn = DateTime.Now;
            userLogin = await this._authenticationService.ValidateUserLoginAsync(userLogin);

            if (userLogin.IsUserAuthenticated)
            {
                await this.AuthenticateUserWithCookie(userLogin);
            }

            if (userLogin.IsUserAuthenticated)
            {
                ajaxReturn.Status = "Success";
                ajaxReturn.Message = userLogin.UserName + " - user authenticated successfully";
            }
            else if (userLogin.IsUserAccountLocked)
            {
                ajaxReturn.Status = "Warning";
                ajaxReturn.Message = "User account locked. Please contact system adminstrator.";
                ajaxReturn.Title = "Sorry";
            }
            else if (userLogin.IsUserAccountNotFound)
            {
                ajaxReturn.Status = "Warning";
                ajaxReturn.Message = "User account not found. Please try again.";
                ajaxReturn.Title = "Sorry";
            }
            else if (!userLogin.IsUserAuthenticated)
            {
                ajaxReturn.Status = "Warning";
                ajaxReturn.Message = "User Name or Password in-correct. Please try again.";
                ajaxReturn.Title = "Sorry";
            }
            return this.Json(ajaxReturn);
        }

        [Route("LogOut")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await this.HttpContext.SignOutAsync();

            dynamic ajaxReturn = new JObject();
            ajaxReturn.Status = "Success";
            ajaxReturn.Message = "You have been successfully logged out. " +
                                    "Current window will be closed now";

            return this.Json(ajaxReturn);
        }

        private async Task AuthenticateUserWithCookie(UserLogin userLogin)
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(1);
            this._httpContextAccessor.HttpContext.Response.Cookies.Append("EmployeeManage.UserAuthenticated", "true", option);

            UserAuthentication userAuthenticationModel = new UserAuthentication();
            userAuthenticationModel.UserName = userLogin.UserName;
            userAuthenticationModel.UserId = userLogin.UserId;
            userAuthenticationModel.LoggedOn = DateTime.Now;
            userAuthenticationModel.AuthenticationExpiresOn = DateTime.Now.AddHours(1);
            userAuthenticationModel.AuthenticationGUID = new Guid().ToString();

            string userData = JsonConvert.SerializeObject(userAuthenticationModel);

            var identity = (ClaimsIdentity)this.HttpContext.User.Identity;

            List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Name, userLogin.UserName),
                                        new Claim(ClaimTypes.NameIdentifier, userLogin.UserName),
                                        new Claim(ClaimTypes.Authentication, "Authenticated"),
                                        new Claim("http://example.org/claims/AuthenticationGUID", "AuthenticationGUID",  userAuthenticationModel.AuthenticationGUID),
                                        new Claim("http://example.org/claims/LoggedOn", "LoggedOn",  userAuthenticationModel.LoggedOn.ToString()),
                                        new Claim("http://example.org/claims/AuthenticationExpiresOn", "AuthenticationExpiresOn",  userAuthenticationModel.AuthenticationExpiresOn.ToString()),
                                        new Claim(ClaimTypes.UserData, userData),
                                    };
            var identityClaims = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

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