namespace CompName.ManageStocks.WebAppMVC.Areas.Authentication.Models.Auth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class UserRoleViewModel
    {
        public int UserRoleId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? CreatedBy { get; set; }

        public string CreatedByUserName { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public string ModifiedByUserName { get; set; }
    }
}