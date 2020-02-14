namespace EmpManage.WebAppMVC.Areas.Home.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EmpManage.CrossCutting.InMemoryCaching;
    using EmpManage.WebAppMVC.Infrastructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
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