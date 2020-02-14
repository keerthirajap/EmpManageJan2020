namespace CompName.ManageStocks.Domain.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class UserGender
    {
        public short? UserGenderId { get; set; }

        public string UserGenderDesc { get; set; }
    }
}