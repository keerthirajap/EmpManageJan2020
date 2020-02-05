namespace EmpManage.WebAppMVC.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class UserGenderViewModel
    {
        public short? UserGenderId { get; set; }

        public string UserGenderDesc { get; set; }
    }
}