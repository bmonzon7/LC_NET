using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace Browserstack
{
    public class BrowserFactory
    {
        static IWebDriver _driver;
        public enum BrowserType { Mobile, Desktop, seleniumGrid }
        public enum Browser { Chrome, ChromeHeadless, FireFox, Edge, Safari }

        //browserType

        //= BrowserType.Desktop;
        //static string USERNAME = "byronmonzongmail1";
        //static string AUTOMATE_KEY = "z8jomQXmMTs6wfmHap8X";

        //= LC ACCOUNT
        static string USERNAME = "bmonzon1";
        static string AUTOMATE_KEY = "FdJH5LULWNhZpELFaFaf";
        static string dateTime = DateTime.Now.ToUniversalTime().ToString();

        public static IWebDriver StartBrowser(Browser browser, BrowserType browserType, string url)
        {



            if ((browser == Browser.Chrome) && (browserType == BrowserType.seleniumGrid))
            {
                ChromeOptions capability = new ChromeOptions();
                capability.AddAdditionalCapability("os", "Windows", true);
                capability.AddAdditionalCapability("os_version", "10", true);
                capability.AddAdditionalCapability("browser", "Chrome", true);
                capability.AddAdditionalCapability("browser_version", "latest", true);
                capability.AddAdditionalCapability("resolution", "1920x1200", true);
                capability.AddAdditionalCapability("browserstack.local", "false", true);
                capability.AddAdditionalCapability("browserstack.debug", "true", true);
                capability.AddAdditionalCapability("browserstack.console", "error", true);
                capability.AddAdditionalCapability("browserstack.selenium_version", "3.5.2", true);
                capability.AddAdditionalCapability("browserstack.user", USERNAME, true);
                capability.AddAdditionalCapability("browserstack.key", AUTOMATE_KEY, true);
                capability.AddAdditionalCapability("build", "build 456", true);
                capability.AddAdditionalCapability("name", "NM_Home_Page_Tests_4", true);
                capability.AddArgument("start-maximized");
                _driver = new RemoteWebDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub/"), capability);
            }


            else if ((browser == Browser.Chrome) && (browserType == BrowserType.Mobile))
            {

                string tmpName = "Samsung Galaxy S9_Demo_Preview_" + dateTime;

                ChromeOptions capability = new ChromeOptions();
                capability.AddAdditionalCapability("os_version", "8.0", true);
                capability.AddAdditionalCapability("device", "Samsung Galaxy S9", true);
                capability.AddAdditionalCapability("real_mobile", "true", true);
                capability.AddAdditionalCapability("project", "Samsung Galaxy S9_Demo_Preview", true);
                capability.AddAdditionalCapability("build", tmpName, true);
                capability.AddAdditionalCapability("name", "BVT_" + dateTime, true);
                capability.AddAdditionalCapability("browserstack.local", "true", true);
                capability.AddAdditionalCapability("browserstack.debug", "true", true);
                capability.AddAdditionalCapability("browserstack.timezone", "Los_Angeles", true);
                capability.AddAdditionalCapability("browserstack.user", USERNAME, true);
                capability.AddAdditionalCapability("browserstack.key", AUTOMATE_KEY, true);
                _driver = new RemoteWebDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub/"), capability);
            }
            /*
			//iPhone X
			if ((browser == Browser.Safari) && (browserType == BrowserType.Mobile))
			{
				SafariOptions capability = new SafariOptions();
				capability.AddAdditionalCapability("os_version", "13");
				capability.AddAdditionalCapability("device", "iPhone 11");
				capability.AddAdditionalCapability("real_mobile", "true");
				capability.AddAdditionalCapability("browserstack.local", "false");
				capability.AddAdditionalCapability("browserstack.debug", "true");
				capability.AddAdditionalCapability("browserstack.console", "verbose");
				capability.AddAdditionalCapability("browserstack.user", USERNAME);
				capability.AddAdditionalCapability("browserstack.key", AUTOMATE_KEY);
				capability.AddAdditionalCapability("build", "07-16-20_Run_5");
				capability.AddAdditionalCapability("name", "NM_Mobile_TestDoctors2SearchByDoc_2CharInput");
				capability.AddAdditionalCapability("browserstack.idleTimeout", 60);
				//capability.AddArgument("start-maximized");
				_driver = new RemoteWebDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub/"), capability);
			}
			*/
            //iPad Pro 12.9 2020
            else if ((browser == Browser.Safari) && (browserType == BrowserType.Mobile))
            {
                string tmpName = "Apple iPhone X_Demo_Preview_" + dateTime;

                SafariOptions capability = new SafariOptions();
                capability.AddAdditionalCapability("os_version", "11");
                capability.AddAdditionalCapability("device", "iPhone X");
                capability.AddAdditionalCapability("real_mobile", "true");
                capability.AddAdditionalCapability("project", "iPhone X_Demo_Preview");
                capability.AddAdditionalCapability("build", tmpName);
                capability.AddAdditionalCapability("name", "NM_Demo_July 17, 2020");
                capability.AddAdditionalCapability("browserstack.local", "true");
                capability.AddAdditionalCapability("browserstack.user", USERNAME);
                capability.AddAdditionalCapability("browserstack.key", AUTOMATE_KEY);
                capability.AddAdditionalCapability("browserstack.idleTimeout", 60);
                //capability.AddArgument("start-maximized");
                _driver = new RemoteWebDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub/"), capability);
            }



            else if ((browser == Browser.FireFox) && (browserType == BrowserType.seleniumGrid))
            {
                //var caps = new RemoteSessionSettings();				
                //caps.AddMetadataSetting("browserName", "Chrome");
                //caps.AddMetadataSetting("platform", "Windows 10");
                //RemoteWebDriver driver = new RemoteWebDriver(new Uri("http://hub.127.0.0.1:80/wd/hub"), caps);
            }

            else if ((browser == Browser.FireFox) && (browserType == BrowserType.Desktop))
            {

                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Directory.GetCurrentDirectory());
                service.HideCommandPromptWindow = true;
                service.Start();

                FirefoxOptions options = new FirefoxOptions();
                options.SetLoggingPreference(LogType.Driver, LogLevel.All);
                //options.AddArguments("--headless");
                options.LogLevel = FirefoxDriverLogLevel.Debug;
                _driver = new FirefoxDriver(service, options);
            }

            else if ((browser == Browser.Chrome) && (browserType == BrowserType.Desktop))
            {
                //string strChromeDriverDir = @"c:\dev\browserDrivers";
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--start-maximized");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                chromeOptions.AddArgument("--disable-popup-blocking");
                //chromeOptions.AddArgument("--incognito");								
                //_driver = new ChromeDriver(strChromeDriverDir, chromeOptions);
                _driver = new ChromeDriver(chromeOptions);
            }

            else if ((browser == Browser.ChromeHeadless) && (browserType == BrowserType.Desktop))
            {
                string strChromeDriverDir = @"c:\dev\browserDrivers";
                ChromeOptions chromeOptions = new ChromeOptions();
                //chromeOptions.BinaryLocation =  @"c:\dev\browserDrivers";
                chromeOptions.AddArgument("--start-maximized");
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                chromeOptions.AddArgument("--disable-popup-blocking");
                //chromeOptions.AddArgument("--incognito");								
                _driver = new ChromeDriver(strChromeDriverDir, chromeOptions);
            }

            else if ((browser == Browser.Safari) && (browserType == BrowserType.Desktop))
            {
                /*
				SafariOptions options = new SafariOptions();
				options.setUseCleanSession(true);

				// For use with SafariDriver:
				System.setProperty("webdriver.safari.noinstall", "true");
				_driver = new SafariDriver(options);
				*/
            }
            //driver.manage().window().maximize();		
            _driver.Navigate().GoToUrl(url);
            Thread.Sleep(1000);
            return _driver;

        }

    }
}


