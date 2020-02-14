namespace CompName.ManageStocks.WebAppMVC.Areas.Home.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CompName.ManageStocks.CrossCutting.InMemoryCaching;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    [Area("Home")]
    [ApplicationAuthorize]
    public class DashboardController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}