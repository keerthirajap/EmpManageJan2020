namespace EmpManage.Domain.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class UserLogin
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public string LoggingIpAddress { get; set; }

        public string LoggingBrowser { get; set; }

        public bool IsUserAuthenticated { get; set; }

        public bool IsUserAccountLocked { get; set; }

        public bool IsUserAccountDisabled { get; set; }

        public bool IsUserAccountNotFound { get; set; }

        public DateTime CreatedOn { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public long? ModifiedBy { get; set; }
    }
}