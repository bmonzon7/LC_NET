using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Browserstack.Interfaces;
using FluentAssertions;

/// <summary>
/// NM HomePage Page Object
/// </summary>
/// 
namespace Browserstack.NM_TestCase_Library.Pages
{
    public class HomePage : Page, IPageController
    {

        private readonly IWebDriver _driver;
        readonly string lnkMyChartXpath = "/html/body/header/nav/div/ul/li[2]/a";
        readonly string lnkHomeXpath = "/html/body/header/nav/div/ul/li[1]/a";
        readonly string lnkMblHomeXpath = "/html/body/header/nav/div/ul/li[1]/a";
        readonly string gblSiteSearchBoxXpath = "/html/body/header/div/div[2]/div/form/input";
        //readonly string gblMobileSiteSearchBoxXpath = "/html/body/header/div/div[1]/div[2]";/html/body/header/div/div[2]/div/form/input
        //readonly string gblMobileSiteSearchBoxButtonXPath =  "";
        readonly string gblSiteSearchBoxButtonXPath = "/html/body/header/div/div[2]/div/form/button[1]";
        readonly string globalSearchResultsXPath = "/html/body/header/div/div[2]/div/div/div/div/div[2]";

        IWebElement lnkMyChart;
        protected IWebElement lnkHome;
        IWebElement utilityBarNavLinks;
        IWebElement gblSiteSearchBoxButton;


        public HomePage(IWebDriver _driver) : base(_driver)
        {
            this._driver = _driver;

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(1);
            Thread.Sleep(12000);

        }

        public string GetMyChartLinkCssStyle()
        {
            lnkMyChart = this.FindElement(By.XPath(lnkMyChartXpath));

            try
            {

                HoverOverElement(lnkMyChart);
                Thread.Sleep(10000);
                string strStyle = lnkMyChart.GetCssValue("text-decoration");
                //Console.WriteLine("The CSS style is: " + strStyle);                    
                return strStyle;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception clicking on MyChart Link - Exception: " + e);
                return null;
            }
        }

        public string GetHomeLinkCssStyle()
        {
            lnkHome = this._driver.FindElement(By.XPath(lnkHomeXpath));

            /*
            WebDriverWait wait = new WebDriverWait( _driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => { return lnkHome = this._driver.FindElement(By.XPath(lnkHomeXpath)); });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            */
            try
            {

                HoverOverElement(lnkHome);
                Thread.Sleep(10000);
                string strStyle = lnkHome.GetCssValue("text-decoration");
                //Console.WriteLine("The CSS style is: " + strStyle);                    
                return strStyle;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception clicking on Home Link - Exception: " + e);
                return null;
            }

        }

        public string ClickMyChartLinkGetURL()
        {
            lnkMyChart = this.FindElement(By.XPath(lnkMyChartXpath));

            try
            {
                lnkMyChart.Click();
                Thread.Sleep(5000);

                /* code for switching to newly opened tab -- START */
                var browserTabs = new List<string>(_driver.WindowHandles);
                var newTab = browserTabs[1];
                var yourUrl = _driver.SwitchTo().Window(newTab).Url;
                /* code for switching to newly opened tab -- END */

                string myChartURL = "";
                myChartURL = yourUrl; //_driver.Url;
                return myChartURL;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception clicking on MyChart Link - Exception: " + e);
                return null;
            }
        }

        public string GetHomeLinkUrl()
        {
            string strHomePageUrl;
            try
            {
                strHomePageUrl = lnkHome.GetAttribute("href");
                return strHomePageUrl;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting the Home Page URL - Exception: " + e);
                return null;
            }
        }

        public void load_complete(int timeout)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));
            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public override string GetElementCssStyleAfterHover(IWebElement elementToCheck)
        {
            IWebElement hvrToThisElement = elementToCheck;
            this.HoverOverElement(hvrToThisElement);
            Thread.Sleep(12000);
            utilityBarNavLinks = this.FindElement(By.CssSelector(".utility-bar a:hover"));
            string strStyle = utilityBarNavLinks.GetCssValue("text-decoration");
            return strStyle;
        }

        public override void HoverOverElement(IWebElement elementToCheck)
        {

            Actions action = new Actions(_driver);
            action.MoveToElement(elementToCheck).Perform();
            Thread.Sleep(9000);
        }

        public void GlobalSiteSearchEnterTerm(string searchTerm)
        {
            string _searchTerm = searchTerm;


            gblSiteSearchBoxButton = this.FindElement(By.XPath(gblSiteSearchBoxButtonXPath));


            //Seleium WAITS
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => { return gblSiteSearchBoxButton; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            gblSiteSearchBoxButton.Click();
            //click on suggested result
            IWebElement glbSiteSearchEntryField = this.FindElement(By.XPath(gblSiteSearchBoxXpath));
            //Seleium WAITS
            WebDriverWait wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait2.Until((d) => { return gblSiteSearchBoxButton; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            glbSiteSearchEntryField.SendKeys(searchTerm);

            IWebElement globalSearchResults = this.FindElement(By.XPath(globalSearchResultsXPath));

            //Seleium WAITS
            WebDriverWait wait3 = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait3.Until((d) => { return globalSearchResults; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(45);

            globalSearchResults.Click();


            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

            IWebElement resultingWebPageTitle = this.FindElement(By.XPath("/html/body/main/div[2]/header/div[2]/div[2]/div/div/h1"));

            string tmpResultH1Value = resultingWebPageTitle.Text;

            tmpResultH1Value.Should().Contain("Athle@tics", "Because we selected Sports Medictine");

            //Seleium WAITS
            WebDriverWait wait4 = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => { return resultingWebPageTitle; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

        }
        public void MobileGlobalSiteSearchEnterTerm(string searchTerm)
        {
            string _searchTerm = searchTerm;


            gblSiteSearchBoxButton = this.FindElement(By.XPath(gblSiteSearchBoxButtonXPath));


            //Seleium WAITS
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => { return gblSiteSearchBoxButton; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            gblSiteSearchBoxButton.Click();
            //click on suggested result
            IWebElement glbSiteSearchEntryField = this.FindElement(By.XPath(gblSiteSearchBoxXpath));
            //Seleium WAITS
            WebDriverWait wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait2.Until((d) => { return gblSiteSearchBoxButton; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            glbSiteSearchEntryField.SendKeys(searchTerm);

            IWebElement globalSearchResults = this.FindElement(By.XPath(globalSearchResultsXPath));

            //Seleium WAITS
            WebDriverWait wait3 = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait3.Until((d) => { return globalSearchResults; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(45);

            globalSearchResults.Click();


            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

            IWebElement resultingWebPageTitle = this.FindElement(By.XPath("/html/body/main/div[2]/header/div[2]/div[2]/div/div/h1"));

            //Seleium WAITS
            WebDriverWait wait4 = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => { return resultingWebPageTitle; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

        }

        public override string GetExpectedPageTitle()
        {
            throw new NotImplementedException();
        }

        public override string GetExpectedUri()
        {
            throw new NotImplementedException();
        }

        public override void TakeScreenshot()
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, List<KeyValuePair<string, string>>> GetExpectedValuesXLS()
        {
            throw new NotImplementedException();
        }
    }



}


