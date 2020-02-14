namespace CompName.ManageStocks.WebAppMVC.Areas.Home.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CompName.ManageStocks.CrossCutting.Constants;
    using CompName.ManageStocks.CrossCutting.InMemoryCaching;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    [Area("Home")]
    public class SampleController : Controller
    {
        public IActionResult Authorization()
        {
            return this.View();
        }

        [ApplicationAuthorize]
        public IActionResult IsUserAuthorized()
        {
            return this.View();
        }

        [ApplicationAuthorize(ApplicationRoles.Administrator)]
        public IActionResult IsUserAdministrator()
        {
            return this.View();
        }

        [ApplicationAuthorize(ApplicationRoles.Administrator, ApplicationRoles.BasicUser)]
        public IActionResult IsUserAdministratorAndBasicUser()
        {
            return this.View();
        }

        [ApplicationAuthorize("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return this.View();
        }

        [ApplicationAuthorize]
        public IActionResult ErrorPage()
        {
            throw new Exception();
        }

        [ApplicationAuthorize]
        public IActionResult AjaxErrorPage()
        {
            return this.View();
        }
    }
}