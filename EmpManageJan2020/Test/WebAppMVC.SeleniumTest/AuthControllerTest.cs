using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace WebAppMVC.SeleniumTest
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class AuthControllerTest
    {
        private IWebDriver chromeWebDriver;
        private IWebDriver fireFoxWebDriver;

        private string url = "https://localhost:44387/";

        [OneTimeSetUp]
        public void SetUp()
        {
            chromeWebDriver = new ChromeDriver(@"C:\WebDriver\");
            fireFoxWebDriver = new FirefoxDriver(@"C:\WebDriver\");
        }

        [Test, Order(1)]
        public void Chrm_Goto_LoginPage_FromHomePage_IfNoAuthentication()
        {
            chromeWebDriver.Url = url;
            Thread.Sleep(3000);

            string currentURL = chromeWebDriver.Url;

            bool isLoginUrl = false;

            if (currentURL.Contains("Login"))
            {
                isLoginUrl = true;
            }

            Assert.IsTrue(isLoginUrl);
        }

        [Test, Order(2)]
        public void FFox_Goto_LoginPage_FromHomePage_IfNoAuthentication()
        {
            fireFoxWebDriver.Url = url;

            Thread.Sleep(3000);

            string currentURL = fireFoxWebDriver.Url;

            bool isLoginUrl = false;

            if (currentURL.Contains("Login"))
            {
                isLoginUrl = true;
            }

            Assert.IsTrue(isLoginUrl);
        }

        [Test, Order(3)]
        public void Chrm_GiveCorrectLogin_Information()
        {
            var giveUserName = chromeWebDriver.FindElement(By.Id("txtUserName"));
            giveUserName.SendKeys("Register");

            var givePassword = chromeWebDriver.FindElement(By.Id("txtPassword"));
            givePassword.SendKeys("Register");

            chromeWebDriver.FindElement(By.Id("btnLogin")).Click();

            Thread.Sleep(3000);

            string currentURL = chromeWebDriver.Url;

            Assert.AreEqual(url, currentURL);
            Assert.AreEqual("Index", chromeWebDriver.Title);
        }

        [Test, Order(4)]
        public void FFox_GiveCorrectLogin_Information()
        {
            var giveUserName = fireFoxWebDriver.FindElement(By.Id("txtUserName"));
            giveUserName.SendKeys("Register");

            var givePassword = fireFoxWebDriver.FindElement(By.Id("txtPassword"));
            givePassword.SendKeys("Register");

            fireFoxWebDriver.FindElement(By.Id("btnLogin")).Click();

            Thread.Sleep(3000);

            string currentURL = fireFoxWebDriver.Url;

            Assert.AreEqual(url, currentURL);
            Assert.AreEqual("Index", fireFoxWebDriver.Title);
        }

        [Test, Order(5)]
        public void Chrm_CanLogout_User()
        {
            chromeWebDriver.FindElement(By.Id("btnLogoutUser")).Click();

            Thread.Sleep(5000);

            string currentURL = chromeWebDriver.Url;

            bool isLoginUrl = false;

            if (currentURL.Contains("Login"))
            {
                isLoginUrl = true;
            }

            Assert.IsTrue(isLoginUrl);
            Assert.AreEqual("Login", chromeWebDriver.Title);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            chromeWebDriver.Quit();
            fireFoxWebDriver.Quit();
        }
    }
}