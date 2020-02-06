namespace EmpManage.WebAppMVC.Areas.Admin.Models.UserManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class UserTitleViewModel
    {
        public short UserTitleId { get; set; }

        public string UserTitleDesc { get; set; }
    }
}