namespace CompName.ManageStocks.WebAppMVC.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Authentication;
    using Newtonsoft.Json;

    public static class WebAppMVCExtensions
    {
        public static UserAuthentication GetLoggedInUserDetails(this ClaimsPrincipal principal)
        {
            UserAuthentication userAuthenticationModel = new UserAuthentication();
            if (principal == null)
            {
                return userAuthenticationModel;
            }

            userAuthenticationModel = JsonConvert.DeserializeObject<UserAuthentication>(principal.FindAll(ClaimTypes.UserData).Select(s => s.Value).FirstOrDefault());

            return userAuthenticationModel;
        }
    }
}