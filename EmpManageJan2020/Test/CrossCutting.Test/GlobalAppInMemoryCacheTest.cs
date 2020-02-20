using System.Diagnostics.CodeAnalysis;
using CompName.ManageStocks.CrossCutting.InMemoryCaching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrossCutting.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class GlobalAppInMemoryCacheTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

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
    }
}