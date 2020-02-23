namespace CompName.ManageStocks.ServiceConcrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Effortless.Net.Encryption;
    using CompName.ManageStocks.CrossCutting.Configuration;
    using CompName.ManageStocks.Domain.Admin;
    using CompName.ManageStocks.Domain.Authentication;
    using CompName.ManageStocks.RepositoryInterface;
    using CompName.ManageStocks.ServiceInterface;
    using CompName.ManageStocks.Domain.Geography;

    public class SharedService : ISharedService
    {
        #region Private Variables

        private readonly AppSetting _appSetting;

        private readonly ISharedRepository _sharedRepository;

        #endregion Private Variables

        #region Constructor

        public SharedService(
                       AppSetting appSetting,
                       ISharedRepository sharedRepository)
        {
            this._appSetting = appSetting;
            this._sharedRepository = sharedRepository;
        }

        #endregion Constructor

        #region Public Methods

        #region Geography

        public async ValueTask<(List<Country> countries, List<State> states, List<City> cities)> GetAllCountryStateCity()
        {
            List<Country> countries = new List<Country>();
            List<State> states = new List<State>();
            List<City> cities = new List<City>();

            var countryStateCityResults = await this._sharedRepository.GetAllCountryStateCity();

            if (countryStateCityResults.Set1 != null)
            {
                countries = countryStateCityResults.Set1.ToList();
            }

            if (countryStateCityResults.Set2 != null)
            {
                states = countryStateCityResults.Set2.ToList();
            }

            if (countryStateCityResults.Set3 != null)
            {
                cities = countryStateCityResults.Set3.ToList();
            }

            return (countries, states, cities);
        }

        #endregion Geography

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}