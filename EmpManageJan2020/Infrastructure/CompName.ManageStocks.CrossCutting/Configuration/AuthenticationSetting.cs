namespace CompName.ManageStocks.CrossCutting.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// App Setting Configuration Model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AuthenticationSetting
    {
        public int PasswordSaltLength { get; set; }

        public string AppCookieName { get; set; }

        public int AuthCookieExpireInHours { get; set; }
    }
}