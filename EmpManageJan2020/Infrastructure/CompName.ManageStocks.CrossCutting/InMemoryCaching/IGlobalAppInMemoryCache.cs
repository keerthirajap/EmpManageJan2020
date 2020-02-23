namespace CompName.ManageStocks.CrossCutting.InMemoryCaching
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Geography;

    public interface IGlobalAppInMemoryCache
    {
        void AddValue(string value);

        string GetValue();

        #region Geography

        ValueTask<List<Country>> GetCountries();

        #endregion Geography
    }
}