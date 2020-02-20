namespace WebAppMVC.Test.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AutoMapperProfileTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void CanConfigureAutoMapperProfileAsync()
        {
            //Act
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper();

            //Assert
            Assert.IsNotNull(mapperConfiguration);
        }
    }
}