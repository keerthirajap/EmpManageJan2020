﻿namespace EmpManage.Domain.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class UserTitle
    {
        public short UserTitleId { get; set; }

        public string UserTitleDesc { get; set; }
    }
}