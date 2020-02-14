namespace CompName.ManageStocks.CrossCutting.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// App Setting Configuration Model
    /// </summary>
    public class AuthenticationSetting
    {
        public int PasswordSaltLength { get; set; }
    }
}