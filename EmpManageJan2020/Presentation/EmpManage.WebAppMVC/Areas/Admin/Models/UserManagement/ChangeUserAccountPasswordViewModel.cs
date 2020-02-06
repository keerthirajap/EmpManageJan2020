namespace EmpManage.WebAppMVC.Areas.Admin.Models.UserManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class ChangeUserAccountPasswordViewModel
    {
        public long UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Compare("Password", ErrorMessage = "Password mismatch, Please try again")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}