using System;
using NUnit.Framework;
using OpenQA.Selenium;
/// <summary>
/// Test Cases for the NM HomePage
/// </summary>
namespace Browserstack
{
    public class BaseTestFixture
    {
        protected static IWebDriver _driver;
        protected static BrowserFactory.BrowserType _browserType { get; set; }
        // = BrowserFactory.BrowserType.Desktop;
        protected static BrowserFactory.Browser _browser { get; set; } //= BrowserFactory.Browser.Chrome;

        protected static string _glbParamName { get; set; }
        protected static string _glbParamValue { get; set; }

    }
}
