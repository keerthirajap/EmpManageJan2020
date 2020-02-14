namespace EmpManage.WebAppMVC.Areas.Employee.Pages.EmployeeManage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize]
#pragma warning disable SA1649 // File name should match first type name
    public class IndexModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        public void OnGet()
        {
        }
    }
}