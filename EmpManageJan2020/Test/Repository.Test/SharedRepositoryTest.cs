using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using CompName.ManageStocks.Domain.Admin;
using CompName.ManageStocks.Domain.Authentication;
using CompName.ManageStocks.Domain.Geography;
using CompName.ManageStocks.RepositoryInterface;
using FizzWare.NBuilder;
using Insight.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Repository.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class SharedRepositoryTest
    {
        #region Private Variables

        private Mock<ISharedRepository> _sharedRepository { get; set; }

        #endregion Private Variables

        #region Constructor

        public SharedRepositoryTest()
        {
            this._sharedRepository = new Mock<ISharedRepository>();
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

            //Act
            var results = await this._sharedRepository.Object.GetAllCountryStateCity();

            //Assert
            Assert.IsTrue(results.Set1 == null);
            Assert.IsTrue(results.Set2 == null);
            Assert.IsTrue(results.Set3 == null);
        }

        #endregion Geography

        #endregion Public Methods
    }
}