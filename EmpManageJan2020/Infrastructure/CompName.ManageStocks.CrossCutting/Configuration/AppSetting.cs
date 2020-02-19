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
    public class AppSetting
    {
        public string AppSettingEnvironment { get; set; }

        public DatabaseSetting DatabaseSetting { get; set; } = new DatabaseSetting();

        public AuthenticationSetting AuthenticationSetting { get; set; } = new AuthenticationSetting();
    }
}