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
    using EmpManage.WebAppMVC.Areas.Admin.Models.UserManagement;
    using EmpManage.WebAppMVC.Areas.Authentication.Models;
    using EmpManage.WebAppMVC.Areas.Authentication.Models.Auth;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    [AutoValidateAntiforgeryToken]
    [AllowAnonymous]
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

        [HttpGet("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync()
        {
            this._httpContextAccessor.HttpContext.Response.Cookies.Delete("EmployeeManage.AuthCookie");

            RegisterUserViewModel registerUserViewModel = new RegisterUserViewModel();
            return await Task.Run(() => this.View(registerUserViewModel));
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync([FromForm] RegisterUserViewModel registerUserViewModel)
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
                    await this.AuthenticateUserWithCookieAsync(userLogin);
                }
            }

            return this.Json(ajaxReturn);
        }

        [HttpGet("IsUserNameExists")]
        public async Task<IActionResult> IsUserNameExistsAsync(string userName)
        {
            User user = new User();
            bool isUserNameExists = false;

            isUserNameExists = await this._authenticationService.IsUserNameExistsAsync(userName);

            if (isUserNameExists)
            {
                return this.Json(false);
            }
            else
            {
                return this.Json(true);
            }
        }

        [HttpGet("IsEmailIdExists")]
        public async Task<IActionResult> IsEmailIdExistsAsync(string emailId)
        {
            User user = new User();
            bool isEmailIdExists = false;

            isEmailIdExists = await this._authenticationService.IsEmailIdExistsAsync(emailId);

            if (isEmailIdExists)
            {
                return this.Json(false);
            }
            else
            {
                return this.Json(true);
            }
        }

        [HttpGet("Login")]
        public async Task<IActionResult> LoginAsync()
        {
            this._httpContextAccessor.HttpContext.Response.Cookies.Delete("EmployeeManage.AuthCookie");

            LoginViewModel loginViewModel = new LoginViewModel();
            return await Task.Run(() => this.View(loginViewModel));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginViewModel loginViewModel)
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
                await this.AuthenticateUserWithCookieAsync(userLogin);
            }

            if (userLogin.IsUserAuthenticated)
            {
                ajaxReturn.Status = "Success";
                ajaxReturn.Title = "Congratulations";
                ajaxReturn.Message = userLogin.UserName + " - user authenticated successfully";
            }
            else if (userLogin.IsUserAccountLocked)
            {
                ajaxReturn.Status = "Warning";
                ajaxReturn.Message = "User account locked. Please contact system adminstrator.";
                ajaxReturn.Title = "Sorry";
            }
            else if (userLogin.IsUserAccountDisabled)
            {
                ajaxReturn.Status = "Warning";
                ajaxReturn.Message = "User account disabled. Please contact system adminstrator.";
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

        [HttpGet("LogOut")]
        public async Task<IActionResult> LogOutAsync()
        {
            await this.HttpContext.SignOutAsync();

            dynamic ajaxReturn = new JObject();
            ajaxReturn.Status = "Success";
            ajaxReturn.Message = "You have been successfully logged out. " +
                                    "Redirecting to Login screen";

            return this.Json(ajaxReturn);
        }

        [HttpGet]
        [Route("[controller]/LoadLoggedUserDetailsPartialView")]

        public async Task<IActionResult> LoadLoggedUserDetailsPartialViewAsync(long loggedInUserId)
        {
            UserAccountViewModel userAccountViewModel = new UserAccountViewModel();

            return await Task.Run(() => this.PartialView("_LoggedUserDetails", userAccountViewModel));
        }

        private async Task AuthenticateUserWithCookieAsync(UserLogin userLogin)
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(1);
            this._httpContextAccessor.HttpContext.Response.Cookies.Append("EmployeeManage.UserAuthenticated", "true", option);

            UserAuthentication userAuthenticationModel = new UserAuthentication();
            userAuthenticationModel.UserName = userLogin.UserName;
            userAuthenticationModel.UserId = userLogin.UserId;
            userAuthenticationModel.LoggedOn = DateTime.Now;
            userAuthenticationModel.AuthenticationExpiresOn = DateTime.Now.AddHours(1);
            userAuthenticationModel.AuthenticationGUID = this.HttpContext.TraceIdentifier;

            string userData = JsonConvert.SerializeObject(userAuthenticationModel);

            var identity = (ClaimsIdentity)this.HttpContext.User.Identity;

            List<Claim> claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Name, userLogin.UserName),
                                        new Claim(ClaimTypes.NameIdentifier, userLogin.UserId.ToString()),
                                        new Claim(ClaimTypes.Authentication, "Form"),
                                        new Claim("UserId",  userLogin.UserId.ToString()),
                                        new Claim("AuthenticationGUID",  userAuthenticationModel.AuthenticationGUID),
                                        new Claim("LoggedOn", userAuthenticationModel.LoggedOn.ToString()),
                                        new Claim("AuthenticationExpiresOn", "AuthenticationExpiresOn",  userAuthenticationModel.AuthenticationExpiresOn.ToString()),
                                        new Claim(ClaimTypes.UserData, userData),
                                    };

            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            claims.Add(new Claim(ClaimTypes.Role, "Manager"));
            claims.Add(new Claim(ClaimTypes.Role, "BasicUser"));

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