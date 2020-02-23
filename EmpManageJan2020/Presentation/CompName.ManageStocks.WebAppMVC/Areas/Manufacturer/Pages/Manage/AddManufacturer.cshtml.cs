namespace CompName.ManageStocks.WebAppMVC.Areas.Manufacturer.Pages.Manage
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
    using CompName.ManageStocks.WebAppMVC.Models.Geography;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Reviewed")]
    public class AddManufacturerModel : PageModel
    {
        #region Private Variables

        private readonly IMapper _mapper;

        private readonly AppSetting _appSetting;

        private readonly IGlobalAppInMemoryCache _globalAppInMemoryCache;

        #endregion Private Variables

        public AddManufacturerModel(
                          IMapper mapper,
                          AppSetting appSetting,
                          IGlobalAppInMemoryCache globalAppInMemoryCache)
        {
            this._mapper = mapper;

            this._appSetting = appSetting;

            this._globalAppInMemoryCache = globalAppInMemoryCache;
        }

        [BindProperty]
        public AddManufacturerViewModel AddManufacturerVM { get; set; }

        [BindProperty]
        public List<CountryViewModel> CountriesVM { get; set; }

        public async Task OnGet()
        {
            List<Country> countries = new List<Country>();
            countries = await this._globalAppInMemoryCache.GetCountries();
            this.CountriesVM = this._mapper.Map<List<CountryViewModel>>(countries);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return await Task.Run(() => this.Page());
            }

            return await Task.Run(() => this.Page());
        }

        public async Task<IActionResult> OnGetSellProduct(int id)
        {
            return await Task.Run(() => this.Page());
        }
    }
}