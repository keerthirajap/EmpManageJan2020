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
            fireFoxWebDriver = new FirefoxDriver(@"C:\WebDriver\");
            chromeWebDriver = new ChromeDriver(@"C:\WebDriver\");
        }

        [Test, Order(1)]
        public void Chrm_Goto_LoginPage_FromHomePage_IfNoAuthentication()
        {
            chromeWebDriver.Url = url;
            Thread.Sleep(2000);

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

            Thread.Sleep(2000);

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

            Thread.Sleep(2500);

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

            Thread.Sleep(2500);

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

        [Test, Order(6)]
        public void FFox_CanLogout_User()
        {
            fireFoxWebDriver.FindElement(By.Id("btnLogoutUser")).Click();

            Thread.Sleep(5000);

            string currentURL = fireFoxWebDriver.Url;

            bool isLoginUrl = false;

            if (currentURL.Contains("Login"))
            {
                isLoginUrl = true;
            }

            Assert.IsTrue(isLoginUrl);
            Assert.AreEqual("Login", fireFoxWebDriver.Title);
        }

        [Test, Order(7)]
        public void Chrm_Login_ValidateMandatoryFields()
        {
            string userNameValidationError;
            string passwordValidationError;
            string popUpError;
            int isUserNameValidationErrorDisplayed;
            int isPasswordValidationErrorDisplayed;

            chromeWebDriver.Url = url;
            Thread.Sleep(1000);

            var giveUserName = chromeWebDriver.FindElement(By.Id("txtUserName"));
            giveUserName.SendKeys("");

            var givePassword = chromeWebDriver.FindElement(By.Id("txtPassword"));
            givePassword.SendKeys("");

            chromeWebDriver.FindElement(By.Id("btnLogin")).Click();

            userNameValidationError = chromeWebDriver.FindElement(By.Id("txtUserName-error")).Text;
            passwordValidationError = chromeWebDriver.FindElement(By.Id("txtPassword-error")).Text;

            Assert.IsTrue(userNameValidationError.Length > 0, "Validation error message not thrown");
            Assert.IsTrue(passwordValidationError.Length > 0, "Validation error message not thrown");

            giveUserName.SendKeys("UserNameTest");
            givePassword.SendKeys("");

            isUserNameValidationErrorDisplayed = chromeWebDriver.FindElements(By.Id("txtUserName-error")).Count;
            passwordValidationError = chromeWebDriver.FindElement(By.Id("txtPassword-error")).Text;

            Assert.IsTrue(isUserNameValidationErrorDisplayed == 0, "Validation error message should not be shown");
            Assert.IsTrue(passwordValidationError.Length > 0, "Validation error message not thrown");

            giveUserName.Clear();
            givePassword.SendKeys("PasswordTest");

            userNameValidationError = chromeWebDriver.FindElement(By.Id("txtUserName-error")).Text;
            isPasswordValidationErrorDisplayed = chromeWebDriver.FindElements(By.Id("txtPassword-error")).Count;

            Assert.IsTrue(userNameValidationError.Length > 0, "Validation error message not thrown");
            Assert.IsTrue(isPasswordValidationErrorDisplayed == 0, "Validation error message should not be shown");

            giveUserName.SendKeys("UserNameTest");
            chromeWebDriver.FindElement(By.Id("btnLogin")).Click();
            Thread.Sleep(500);
            popUpError = chromeWebDriver.FindElement(By.Id("modalMessageShowPopUpMessage")).Text;
            Assert.IsTrue(popUpError.Length > 0, "Validation error message not thrown");
            chromeWebDriver.FindElement(By.Id("btnCloseMessageShowPopUpMessage")).Click();
        }

        [Test, Order(9)]
        public void Chrm_Login_ValidateLockUnlock_Account()
        {
            chromeWebDriver.Url = url;
            string popUpError;

            Thread.Sleep(1000);

            var giveUserName = chromeWebDriver.FindElement(By.Id("txtUserName"));
            giveUserName.Clear();

            var givePassword = chromeWebDriver.FindElement(By.Id("txtPassword"));
            givePassword.Clear();

            giveUserName.SendKeys("LockAccount");
            givePassword.SendKeys("LockAccount");
            chromeWebDriver.FindElement(By.Id("btnLogin")).Click();

            Thread.Sleep(5000);

            chromeWebDriver.FindElement(By.Id("btnLogoutUser")).Click();

            Thread.Sleep(1000);

            chromeWebDriver.Url = url;

            Thread.Sleep(1000);

            giveUserName = chromeWebDriver.FindElement(By.Id("txtUserName"));
            giveUserName.Clear();

            givePassword = chromeWebDriver.FindElement(By.Id("txtPassword"));
            givePassword.Clear();

            giveUserName.SendKeys("LockAccount");
            givePassword.SendKeys("LockAccountTest");
            chromeWebDriver.FindElement(By.Id("btnLogin")).Click();

            Thread.Sleep(1000);

            popUpError = chromeWebDriver.FindElement(By.Id("modalMessageShowPopUpMessage")).Text.Trim();
            Assert.IsTrue(popUpError == "User Name or Password in-correct. Please try again.", "Validation error message not thrown");
            chromeWebDriver.FindElement(By.Id("btnCloseMessageShowPopUpMessage")).Click();
            chromeWebDriver.FindElement(By.Id("btnLogin")).Click();

            Thread.Sleep(1000);

            popUpError = chromeWebDriver.FindElement(By.Id("modalMessageShowPopUpMessage")).Text.Trim();
            Assert.IsTrue(popUpError == "User Name or Password in-correct. Please try again.", "Validation error message not thrown");
            chromeWebDriver.FindElement(By.Id("btnCloseMessageShowPopUpMessage")).Click();
            chromeWebDriver.FindElement(By.Id("btnLogin")).Click();

            Thread.Sleep(1000);

            popUpError = chromeWebDriver.FindElement(By.Id("modalMessageShowPopUpMessage")).Text.Trim();
            Assert.IsTrue(popUpError == "User Name or Password in-correct. Please try again.", "Validation error message not thrown");
            chromeWebDriver.FindElement(By.Id("btnCloseMessageShowPopUpMessage")).Click();
            chromeWebDriver.FindElement(By.Id("btnLogin")).Click();

            Thread.Sleep(1000);
            popUpError = chromeWebDriver.FindElement(By.Id("modalMessageShowPopUpMessage")).Text.Trim();
            Assert.IsTrue(popUpError == "User account locked. Please contact system adminstrator.", "Validation error message not thrown");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            chromeWebDriver.Quit();
            fireFoxWebDriver.Quit();
        }
    }
}