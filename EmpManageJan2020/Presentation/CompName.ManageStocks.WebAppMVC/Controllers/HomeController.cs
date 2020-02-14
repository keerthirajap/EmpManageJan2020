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

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Index()
        {
            this._logger.LogInformation("Hello, this is the index!");
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            this.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, RequestTime = DateTime.Now });
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return this.View();
        }
    }
}