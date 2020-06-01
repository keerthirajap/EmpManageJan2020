using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using OpenQA.Selenium;

namespace WebAppMVC.SpecSeleTest.Setup
{
    public class Browser
    {
        protected string WebSiteUrl => $"{ConfigurationManager.AppSettings["webSiteHost"]}:{ConfigurationManager.AppSettings["webSitePort"]}";

        public string Title => WebDriver.Title;

        protected IWebDriver WebDriver => Startup.WebDriver;

        protected void Goto(string url)
        {
            WebDriver.Url = url;
        }
    }
}