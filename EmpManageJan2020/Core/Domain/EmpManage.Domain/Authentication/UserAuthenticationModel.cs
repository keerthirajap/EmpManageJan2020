namespace EmpManage.Domain.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class UserAuthenticationModel
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public DateTime LoggedOn { get; set; }

        public DateTime AuthenticationExpiresOn { get; set; }

        public string AuthenticationGUID { get; set; }
    }
}