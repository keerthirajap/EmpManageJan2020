using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace WebAppMVC.SeleniumTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IWebDriver webDriver = new FirefoxDriver(@"C:\WebDriver\geckodriver.exe");
            webDriver.Url = "https://www.google.com/";
        }
    }
}