namespace EmpManage.WebAppMVC.Areas.Authentication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Remote(
                action: "IsUserNameExists",
                controller: "Auth",
                areaName: "Authentication",
                ErrorMessage = "User Name already exists.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Email Id")]
        [EmailAddress(ErrorMessage = "Invalid Email Id")]
        [Remote(
                action: "IsEmailIdExists",
                controller: "Auth",
                areaName: "Authentication",
                ErrorMessage = "Email Id already exists.")]
        public string EmailId { get; set; }

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