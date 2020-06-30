using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using CompName.ManageStocks.CrossCutting.Configuration;
using CompName.ManageStocks.Domain.Admin;
using CompName.ManageStocks.Domain.Authentication;
using CompName.ManageStocks.Domain.Geography;
using CompName.ManageStocks.RepositoryInterface;
using CompName.ManageStocks.ServiceConcrete;
using CompName.ManageStocks.ServiceInterface;
using FizzWare.NBuilder;
using Insight.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Service.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class SharedServiceTest
    {
        #region Private Variables

        private Mock<ISharedRepository> _sharedRepository { get; set; }
        private AppSetting _appSetting { get; set; }
        private SharedService _sharedService { get; set; }

        private List<Country> Countries { get; set; }

        private List<State> States { get; set; }

        private List<City> cities { get; set; }

        #endregion Private Variables

        #region Constructor

        public SharedServiceTest()
        {
            this._sharedRepository = new Mock<ISharedRepository>();
            this._appSetting = Builder<AppSetting>.CreateListOfSize(1).Build().ToList().FirstOrDefault();
            this.Countries = Builder<Country>.CreateListOfSize(100).Build().ToList();
            this.States = Builder<State>.CreateListOfSize(100).Build().ToList();
            this.cities = Builder<City>.CreateListOfSize(100).Build().ToList();
        }

        #endregion Constructor

        #region Public Methods

        #region Geography

        [TestMethod]
        public async Task CanGetAllCountryStateCity()
        {
            //Arrange
            this._sharedRepository
                .Setup(m => m.GetAllCountryStateCity())
                .ReturnsAsync(new Results<Country, State, City>());

            this._sharedService = new SharedService(this._appSetting, this._sharedRepository.Object);

            //Act
            var countryStateCityResults = await this._sharedService.GetAllCountryStateCity();

            //Assert
            Assert.IsTrue(countryStateCityResults.countries != null);
            Assert.IsTrue(countryStateCityResults.states != null);
            Assert.IsTrue(countryStateCityResults.states != null);
        }

        #endregion Geography

        #endregion Public Methods
    }
}