namespace EmpManage.WebAppMVC.Areas.Admin.Models.UserManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class UpdateUserAccountViewModel
    {
        public long UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Title")]
        [Range(51, 56, ErrorMessage = "Please select valid user title")]
        public short? UserTitleId { get; set; }

        public string UserTitleDesc { get; set; }

        public List<UserTitleViewModel> UserTitles { get; set; } = new List<UserTitleViewModel>();

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Id")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [Display(Name = "Gender")]
        [Range(11, 12, ErrorMessage = "Please select valid user gender")]
        public short? UserGenderId { get; set; }

        public string UserGenderDesc { get; set; }

        public List<UserGenderViewModel> UserGenders { get; set; } = new List<UserGenderViewModel>();

        [Display(Name = "Account Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Account Locked")]
        public bool IsLocked { get; set; }

        public DateTime CreatedOn { get; set; }

        public long CreatedBy { get; set; }

        public string CreatedByUserName { get; set; }

        public DateTime ModifiedOn { get; set; }

        public long ModifiedBy { get; set; }

        public string ModifiedByUserName { get; set; }
    }
}