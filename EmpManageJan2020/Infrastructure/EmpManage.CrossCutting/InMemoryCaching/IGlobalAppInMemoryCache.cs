namespace EmpManage.CrossCutting.InMemoryCaching
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public interface IGlobalAppInMemoryCache
    {
        void AddValue(string value);

        object GetValue();
    }
}