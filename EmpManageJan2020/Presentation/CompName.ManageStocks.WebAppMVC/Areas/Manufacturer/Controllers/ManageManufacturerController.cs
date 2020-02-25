namespace CompName.ManageStocks.WebAppMVC.Areas.Manufacturer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using CompName.ManageStocks.CrossCutting.Configuration;
    using CompName.ManageStocks.CrossCutting.InMemoryCaching;
    using CompName.ManageStocks.Domain.Geography;
    using CompName.ManageStocks.WebAppMVC.Areas.Manufacturer.Models;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.Security;
    using CompName.ManageStocks.WebAppMVC.Models.Geography;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [AutoValidateAntiforgeryToken]
    [Area("Manufacturer")]
    [ApplicationAuthorize]
    public class ManageManufacturerController : Controller
    {
        #region Private Variables

        private readonly IMapper _mapper;

        private readonly AppSetting _appSetting;

        private readonly IGlobalAppInMemoryCache _globalAppInMemoryCache;

        #endregion Private Variables

        #region Constructor

        public ManageManufacturerController(
                          IMapper mapper,
                          AppSetting appSetting,
                          IGlobalAppInMemoryCache globalAppInMemoryCache)
        {
            this._mapper = mapper;

            this._appSetting = appSetting;

            this._globalAppInMemoryCache = globalAppInMemoryCache;
        }

        #endregion Constructor

        #region Public Methods

        #region Manage Manufacturer

        [HttpGet]
        [Route("[controller]/AddManufacturer")]
        public async ValueTask<IActionResult> GetAddManufacturerViewAsync()
        {
            AddManufacturerViewModel addManufacturerViewModel = new AddManufacturerViewModel();
            List<CountryViewModel> countriesVM = new List<CountryViewModel>();
            List<Country> countries = new List<Country>();
            countries = await this._globalAppInMemoryCache.GetCountries();
            countriesVM = this._mapper.Map<List<CountryViewModel>>(countries);

            addManufacturerViewModel.CountriesVM = countriesVM;

            return await Task.Run(() => this.View("AddManufacturer", addManufacturerViewModel));
        }

        #endregion Manage Manufacturer

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}