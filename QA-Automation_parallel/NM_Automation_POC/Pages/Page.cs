using OpenQA.Selenium;
using Browserstack.Interfaces;
using System.Collections.Generic;

namespace Browserstack
{
    public abstract class Page : PageController, IPageExpedResults
    {
        protected Page(IWebDriver _driver) : base(_driver) { }
        public abstract string GetExpectedPageTitle();
        public abstract string GetExpectedUri();

        public abstract Dictionary<string, List<KeyValuePair<string, string>>> GetExpectedValuesXLS();
        public abstract void TakeScreenshot();
    }
}