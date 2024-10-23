using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Browserstack.Interfaces;

namespace Browserstack
{
    
    public class PageController : IPageController
    {
        private readonly IWebDriver _driver;
        protected PageController(IWebDriver driver)
        {
            this._driver = driver;
        }

       
        public string GetActualPageTitle()
        {
            return _driver.Title;
        }

        public string GetActualUri()
        {
            return _driver.Url;
        }

        public virtual string GetElementCssStyleAfterHover(IWebElement elementToCheck)
        {

            return null ;
        }

        public virtual void HoverOverElement(IWebElement elementToCheck)
        {
           
        }

        protected IWebElement FindElement(string cssSelector)
        {
            return this._driver.FindElement(By.CssSelector(cssSelector));
        }
        protected IWebElement FindElement(By by)
        {
            return this._driver.FindElement(by);
        }

        protected ReadOnlyCollection<IWebElement> FindElements(string cssSelector)
        {
            return this._driver.FindElements(By.CssSelector(cssSelector));
        }
        protected ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return this._driver.FindElements(by);
        }
       
        

    }
}

