namespace CompName.ManageStocks.WebAppMVC.Infrastructure.Security
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    [ExcludeFromCodeCoverage]
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