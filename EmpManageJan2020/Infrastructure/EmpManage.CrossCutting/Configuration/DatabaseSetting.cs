namespace EmpManage.CrossCutting.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// App Setting Configuration Model
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class DatabaseSetting
    {
        public string SqlDbConnection { get; set; }
    }
}