namespace EmpManage.WebAppMVC.Areas.Authentication.Models.Auth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}