namespace CompName.ManageStocks.WebAppMVC.Areas.Employee.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    [Area("Employee")]
    [ApplicationAuthorize]
    public class EmployeeManageController : Controller
    {
        public IActionResult CreateEmployee()
        {
            return this.View();
        }
    }
}