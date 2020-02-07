namespace EmpManage.WebAppMVC.Infrastructure.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class CustomeUserPrincipal : IPrincipal
    {
        public long UserId { get; set; }

        public CustomeUserPrincipal(ClaimsIdentity principal)
        {
        }

        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        // additional properties
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class BaseIdentity : ClaimsIdentity
    {
        public BaseIdentity()
        {
        }

        public BaseIdentity(IEnumerable<Claim> claims, string authenticationType)
        {
        }

        public long UserId { get; set; }

        public override bool IsAuthenticated { get; }
    }
}