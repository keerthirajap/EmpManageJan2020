using System.Diagnostics.CodeAnalysis;
using CompName.ManageStocks.CrossCutting.InMemoryCaching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrossCutting.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class GlobalAppInMemoryCacheTest
    {
        #region Constructor

        public GlobalAppInMemoryCacheTest()
        {
        }

        #endregion Constructor

        #region Public Methods

        [TestMethod]
        public void CaAddValue()
        {
            //Arrange
            GlobalAppInMemoryCache.Instance.AddValue("Test");

            //Act
            var value = GlobalAppInMemoryCache.Instance.GetValue();

            //Assert
            Assert.AreEqual("Test", "Test");
        }

        #endregion Public Methods
    }
}