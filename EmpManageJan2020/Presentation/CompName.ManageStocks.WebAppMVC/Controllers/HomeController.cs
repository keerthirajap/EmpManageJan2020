namespace CompName.ManageStocks.WebAppMVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using CompName.ManageStocks.WebAppMVC.Models;
    using Microsoft.AspNetCore.Http;

    public class HomeController : Controller
    {
        #region Private Variables

        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion Private Variables

        #region Constructor

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessor;
        }

        #endregion Constructor

        #region Public Methods

        public async ValueTask<IActionResult> Index()
        {
            this._logger.LogInformation("Hello, this is the index!");
            return await Task.Run(() => this.View("Index"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async ValueTask<IActionResult> Error()
        {
            this._httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorVM = new ErrorViewModel();
            errorVM.RequestId = this._httpContextAccessor.HttpContext.TraceIdentifier;
            errorVM.RequestTime = DateTime.Now;
            return await Task.Run(() => this.View("Error", errorVM));
        }

        [HttpGet("AccessDenied")]
        public async ValueTask<IActionResult> AccessDenied()
        {
            return await Task.Run(() => this.View("AccessDenied"));
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}