﻿namespace EmpManage.WebAppMVC.Areas.Admin.Models.UserManagement.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class UserAccountDetailsDTO
    {
        public UpdateUserAccountViewModel UserDetails { get; set; }

        public List<UserLoginViewModel> UserInCorrectAuthLogs { get; set; }

        public List<UserLoginViewModel> UserLoggingLogs { get; set; }
    }
}