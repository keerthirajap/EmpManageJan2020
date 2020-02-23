namespace CompName.ManageStocks.CrossCutting.InMemoryCaching
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Geography;
    using Insight.Database;
    using Microsoft.Extensions.Caching.Memory;

    public class GlobalAppInMemoryCache : IGlobalAppInMemoryCache
    {
        #region Private Variables

        private readonly DbConnection _sqlConnection;

        private readonly MemoryCache _applicationData = new MemoryCache(new MemoryCacheOptions());

        private readonly object _padlock = new object();

        #endregion Private Variables

        #region Constructor

        public GlobalAppInMemoryCache(string sqlConnectionString)
        {
            this._sqlConnection = new SqlConnection(sqlConnectionString);
        }

        #endregion Constructor

        #region Public Methods

        public void AddValue(string value)
        {
            lock (this._padlock)
            {
                this._applicationData.Set("Value", value, DateTimeOffset.MaxValue);
            }
        }

        public string GetValue()
        {
            string value;

            lock (this._padlock)
            {
                this._applicationData.TryGetValue("Value", out value);
            }

            return value;
        }

        #endregion Public Methods

        #region Geography

        public async ValueTask<List<Country>> GetCountries()
        {
            List<Country> countries = new List<Country>();

            lock (this._padlock)
            {
                this._applicationData.TryGetValue("Countries", out countries);
            }

            if (countries != null && countries.Count > 0)
            {
                return countries;
            }

            countries = (await this._sqlConnection.QuerySqlAsync<Country>(
                                 @" SELECT [CountryId]
                                      ,[ShortName]
                                      ,[CountryName]
                                      ,[CountryPhoneCode]
                                  FROM[dbo].[Country]",
                                 new { Name = "IPA" })).ToList();

            lock (this._padlock)
            {
                this._applicationData.Set("Countries", countries, DateTimeOffset.MaxValue);
            }

            return countries;
        }

        #endregion Geography
    }
}