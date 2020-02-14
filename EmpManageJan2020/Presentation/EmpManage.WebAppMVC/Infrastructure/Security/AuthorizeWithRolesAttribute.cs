namespace EmpManage.WebAppMVC.Infrastructure.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class ApplicationAuthorizeAttribute : AuthorizeAttribute
    {
        public ApplicationAuthorizeAttribute(params string[] roles)
        {
            if (roles.Length > 0)
            {
                this.Roles = string.Join(",", roles);
            }
        }
    }
}