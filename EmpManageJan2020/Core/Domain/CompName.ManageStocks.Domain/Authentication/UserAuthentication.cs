namespace CompName.ManageStocks.Domain.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserAuthentication
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public DateTime LoggedOn { get; set; }

        public DateTime AuthenticationExpiresOn { get; set; }

        public string AuthenticationGUID { get; set; }
    }
}