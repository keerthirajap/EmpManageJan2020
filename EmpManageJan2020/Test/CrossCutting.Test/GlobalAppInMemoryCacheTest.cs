using System.Diagnostics.CodeAnalysis;
using CompName.ManageStocks.CrossCutting.InMemoryCaching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CrossCutting.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class GlobalAppInMemoryCacheTest

    {
        #region Private Variables

        private Mock<IGlobalAppInMemoryCache> _globalAppInMemoryCache { get; set; }

        #endregion Private Variables

        #region Constructor

        public GlobalAppInMemoryCacheTest()
        {
            this._globalAppInMemoryCache = new Mock<IGlobalAppInMemoryCache>();
        }

        #endregion Constructor

        #region Public Methods

        [TestMethod]
        public void CaAddAndGetValue()
        {
            //Arrange
            this._globalAppInMemoryCache
                .Setup(m => m.AddValue(It.IsAny<string>()));
            this._globalAppInMemoryCache
              .Setup(m => m.GetValue())
              .Returns("Test");

            //Act
            this._globalAppInMemoryCache.Object.AddValue("");
            var results = this._globalAppInMemoryCache.Object.GetValue();

            //Assert
            Assert.IsTrue(results == "Test");
        }

        #endregion Public Methods
    }
}