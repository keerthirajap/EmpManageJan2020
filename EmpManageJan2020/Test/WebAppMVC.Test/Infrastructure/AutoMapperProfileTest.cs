namespace WebAppMVC.Test.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AutoMapperProfileTest
    {
        private IMapper _mapper { get; set; }

        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public async Task CanConfigureAutoMapperProfileAsync()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper();

            Assert.IsNotNull(mapperConfiguration);
        }
    }
}