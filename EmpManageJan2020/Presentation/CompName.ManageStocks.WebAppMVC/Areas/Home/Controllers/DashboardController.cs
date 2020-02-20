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
        #region Private Variables

        #endregion Private Variables

        #region Constructor

        #endregion Constructor

        #region Public Methods

        [Route("")]
        public async ValueTask<IActionResult> Index()
        {
            return await Task.Run(() => this.View("Index"));
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}