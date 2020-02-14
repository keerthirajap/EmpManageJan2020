namespace CompName.ManageStocks.CrossCutting.InMemoryCaching
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IGlobalAppInMemoryCache
    {
        void AddValue(string value);

        object GetValue();
    }
}