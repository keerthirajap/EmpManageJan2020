namespace EmpManage.CrossCutting.InMemoryCaching
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Extensions.Caching.Memory;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public abstract class GlobalAppInMemoryCacheBase
    {
        private static readonly MemoryCache ApplicationData = new MemoryCache(new MemoryCacheOptions());

        private static readonly object Padlock = new object();

        public GlobalAppInMemoryCacheBase()
        {
        }

        protected void AddValue(string value)
        {
            lock (Padlock)
            {
                ApplicationData.Set("Value", value, DateTimeOffset.MaxValue);
            }
        }

        protected object GetValue()
        {
            string value;

            lock (Padlock)
            {
                ApplicationData.TryGetValue("Value", out value);
            }

            return value;
        }
    }
}