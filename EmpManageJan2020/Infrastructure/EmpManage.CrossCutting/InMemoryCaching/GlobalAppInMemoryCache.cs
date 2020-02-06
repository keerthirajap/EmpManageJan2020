namespace EmpManage.CrossCutting.InMemoryCaching
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Microsoft.Extensions.Caching.Memory;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class GlobalAppInMemoryCache : GlobalAppInMemoryCacheBase, IGlobalAppInMemoryCache
    {
        #region Singleton

        protected GlobalAppInMemoryCache()
        {
        }

        public static GlobalAppInMemoryCache Instance
        {
            get
            {
                return Nested.Instance;
            }
        }

        public virtual new void AddValue(string value)
        {
            base.AddValue(value);
        }

        public virtual new object GetValue()
        {
            return base.GetValue();
        }

        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "Reviewed.")]
        private class Nested
        {
            internal static readonly GlobalAppInMemoryCache Instance = new GlobalAppInMemoryCache();

            static Nested()
            {
            }
        }

        #endregion Singleton
    }
}