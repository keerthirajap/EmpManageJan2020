namespace EmpManage.WebAppMVC.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using EmpManage.Domain.Authentication;
    using Newtonsoft.Json;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public static class WebAppMVCExtensions
    {
        public static UserAuthentication GetLoggedInUserDetails(this ClaimsPrincipal principal)
        {
            UserAuthentication userAuthenticationModel = new UserAuthentication();
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            userAuthenticationModel = JsonConvert.DeserializeObject<UserAuthentication>(principal.FindAll(ClaimTypes.UserData).Select(s => s.Value).FirstOrDefault());

            return userAuthenticationModel;
        }
    }
}