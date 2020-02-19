using CompName.ManageStocks.CrossCutting.InMemoryCaching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrossCutting.Test
{
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
            GlobalAppInMemoryCache.Instance.AddValue("Test");

            var value = GlobalAppInMemoryCache.Instance.GetValue();

            Assert.AreEqual("Test", "Test");
        }
    }
}