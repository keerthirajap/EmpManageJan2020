using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace WebAppMVC.SpecSeleTest
{
    [TestFixture]
    public class AuthenticationTest
    {
        private IWebDriver webDriver;
        private string url = "https://localhost:44387/";

        [Test, Order(1)]
        public void StartApplication()
        {
            webDriver = new ChromeDriver(@"C:\WebDriver\");
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(url);
        }
    }
}