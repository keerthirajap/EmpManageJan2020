namespace CompName.ManageStocks.RepositoryInterface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Admin;
    using CompName.ManageStocks.Domain.Authentication;
    using CompName.ManageStocks.Domain.Geography;
    using Insight.Database;

    public interface ISharedRepository
    {
        #region Geography

        [Sql("[dbo].[P_GetAllCountryStateCity]")]
        Task<Results<Country, State, City>> GetAllCountryStateCity();

        #endregion Geography
    }
}