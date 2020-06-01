using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace WebAppMVC.SpecSeleTest.Setup
{
    [Binding]
    public static class StartUp
    {
        private static readonly SeleniumWebDriverStartup SeleniumTest =
             new SeleniumWebDriverStartup("hELLO");

        public static IWebDriver WebDriver => SeleniumTest.WebDriver;

        [BeforeTestRun]
        public static void Start()
        {
            SeleniumTest.Initialize();
        }

        [AfterTestRun]
        public static void Down()
        {
            SeleniumTest.Cleanup();
        }
    }
}