namespace EmpManage.WebAppMVC.Areas.Authentication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EmpManage.ServiceInterface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    [AutoValidateAntiforgeryToken]
    [Area("Authentication")]
    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IAuthenticationService _authenticationService;

        public AuthController(
                               IHttpContextAccessor httpContextAccessor,
                               IAuthenticationService authenticationService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._authenticationService = authenticationService;
        }

        [Route("")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var vvv = await this._authenticationService.RegisterUserAsync(new Domain.User());
            return await Task.Run(() => this.View());
        }
    }
}