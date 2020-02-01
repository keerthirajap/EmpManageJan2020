namespace EmpManage.WebAppMVC.Areas.Authentication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EmpManage.Domain;
    using EmpManage.ServiceInterface;
    using EmpManage.WebAppMVC.Areas.Authentication.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    [AutoValidateAntiforgeryToken]
    [Area("Authentication")]
    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private readonly IAuthenticationService _authenticationService;

        public AuthController(
                               IMapper mapper,
                               IHttpContextAccessor httpContextAccessor,
                               IAuthenticationService authenticationService)
        {
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            this._authenticationService = authenticationService;
        }

        [Route("")]
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
            if (!this.ModelState.IsValid)
            {
                return await Task.Run(() => this.View(registerUserViewModel));
            }

            dynamic ajaxReturn = new JObject();
            var user = this._mapper.Map<User>(registerUserViewModel);
            user.Password = "123";
            user.PasswordHash = "123";
            user.PasswordSalt = "123";

            var userCreationSuccess = await this._authenticationService.RegisterUserAsync(user);

            return this.Json(ajaxReturn);
        }
    }
}