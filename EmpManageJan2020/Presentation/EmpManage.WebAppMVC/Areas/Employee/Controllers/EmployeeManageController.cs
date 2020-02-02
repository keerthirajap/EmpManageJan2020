namespace EmpManage.WebAppMVC.Areas.Employee.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    [AutoValidateAntiforgeryToken]
    [Area("Employee")]
    [Authorize]
    public class EmployeeManageController : Controller
    {
        public IActionResult CreateEmployee()
        {
            return this.View();
        }
    }
}