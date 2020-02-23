namespace CompName.ManageStocks.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Admin;
    using CompName.ManageStocks.Domain.Authentication;
    using CompName.ManageStocks.Domain.Geography;

    public interface ISharedService
    {
        #region Geography

        ValueTask<(List<Country> countries, List<State> states, List<City> cities)> GetAllCountryStateCity();

        #endregion Geography
    }
}