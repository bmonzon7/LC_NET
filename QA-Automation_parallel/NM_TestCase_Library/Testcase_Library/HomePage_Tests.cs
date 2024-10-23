using System;
using NUnit.Framework;
using FluentAssertions;
using static Browserstack.BrowserFactory;
using Browserstack.NM_TestCase_Library.Pages;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

/// <summary>
/// Test Cases for the NM HomePage
/// </summary>
namespace Browserstack.NM_TestCase_Library.Tescase_Library
{
    [TestFixture]
    public class HomePage_Tests : BaseTestFixture
    {        
        private string _nmHomePageURL =  "https://uat.nm.org";
        public static HomePage _pgNMHomePage;   
      
        [SetUp]
        public void Init()
        {
            PageInit();
            //_driver = new FirefoxDriver();
        }

        [TearDown]
        public void Dispose()
        {
            _driver.Quit();
        }

       
        public HomePage PageInit()
        {
            // _browserType = BrowserType.Mobile;
            // _browser = Browser.Safari;
            foreach (var name in TestContext.Parameters.Names)
            {
                if (name == "browser" && (TestContext.Parameters.Get(name) == "Chrome"))
                {
                    _browser = Browser.Chrome;

                }
                if (name == "browserType" && (TestContext.Parameters.Get(name) == "Desktop"))
                {
                    _browserType = BrowserType.Desktop;

                }
            }

           try
            {
                _driver = StartBrowser(_browser, _browserType, _nmHomePageURL);               
                _pgNMHomePage = new HomePage(_driver);
                return _pgNMHomePage;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e);
                //Assert.Fail("Could Not Instanciate the WebDriver object");
                //Console.WriteLine("Exception: " + e);
                return null;
            }                     
        }        
       
       // [TestCase("browser.chrome.desktop")]
      //  [TestCase("browser.chrome.grid")]
        [Test, Order(1)]
        [Category("BVT - Home Page Tests")]      
        public void TestHomePageLoads()
        {
           


                //Console.WriteLine("Parameter: {0} = {1}", name, TestContext.Parameters.Get(name));
            
           

   

            Console.WriteLine("TestName: " + TestContext.CurrentContext.Test.MethodName.ToString());
            try
            {
                string actualUri = _pgNMHomePage.GetActualUri();
                actualUri.Should().Be("https://nm.org");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in TestHomePageLoads() - Exception: " + e);
               
            }       
        
        }
        
       
        /*
        public void TestPrototype()
        {
            try
            {
                _pgNMHomePage.GoHome();               
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in GoHome() -- Message: " + e);
            }

        }

        */
       

        [Test, Order(2)]
        [Category("BVT - Home Page Tests")]
        public void TestHomeLinkStyle()
        {
            Console.WriteLine("TestName: " + TestContext.CurrentContext.Test.MethodName.ToString());
            //_pgNMHomePage.GoHome();
            string cssStyle = _pgNMHomePage.GetHomeLinkCssStyle();
            Console.WriteLine("The Style after hovering is: " + cssStyle);
        }

        [Test, Order(3)]
        [Category("BVT - Home Page Tests")]
        public void TestMyChartLinkStyle()
        {
            Console.WriteLine("TestName: " + TestContext.CurrentContext.Test.MethodName.ToString());            
            string cssStyle = _pgNMHomePage.GetMyChartLinkCssStyle();
            Console.WriteLine("The Style after hovering is: " + cssStyle);

        }

        [Test, Order(4)]
        [Category("BVT - Home Page Tests")]
        public void TestHomeLink()
        {
            Console.WriteLine("TestName: " + TestContext.CurrentContext.Test.MethodName.ToString());
            foreach (var name in TestContext.Parameters.Names)
            {
                Console.WriteLine("Parameter: {0} =  {1}", name, TestContext.Parameters.Get(name));
            }

            string expectedURL = "https://nm.org1";
            string strHomePageURL = _pgNMHomePage.GetHomeLinkUrl();
            Console.WriteLine("The NM HomePage is: " + strHomePageURL);
            strHomePageURL.Should().Be(expectedURL, "Because this is the way we chose");
        }

        [Test, Order(5)]
        [Category("BVT - Home Page Tests")]
        public void TestMyChartLink()
        {
            Console.WriteLine("TestName: " + TestContext.CurrentContext.Test.MethodName.ToString());
            _pgNMHomePage = new HomePage(_driver);
            string myChartExpectedURL = "https://nm.org/Mychart";
            string actualURL = _pgNMHomePage.ClickMyChartLinkGetURL();
            actualURL.Should().Be(myChartExpectedURL, "Because this is what MyChart points to").And.StartWith("www.").And.EndWith(".org");
        }

        /*
        [Test, Order(5)]
         // [Category("TestRunner")]
        //[Ignore("Not testing this at this time")]
        public void ReadExcelInput()
        {
            string strFileName = @"C:\DEV\Externals\TestData.xlsx";
            try
            {               
                Excel_ReaderWriter excel_Reader = new Excel_ReaderWriter();
                excel_Reader.ReadXLS(strFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception trying to read from Excel -- Exception: " + e);
            }
        }

        [Test]
        [Category("READ-EXCEL")]
        //[Ignore("Not testing this at this time")]
        public void ReadDocSearchExcelInput()
        {
            string strFileName = @"C:\Users\bmonzon\source\repos\QA-Automation\NM_Automation_POC\Externals\TestDataDocSearch.xlsx";
            try
            {
                Excel_ReaderDocSearch excel_Reader = new Excel_ReaderDocSearch();
                excel_Reader.ReadXLS(strFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception trying to read from Excel -- Exception: " + e);
            }
        }

        */
       
        [Test, Order(6)]
        [Category("BVT - Home Page Tests")]
       // [Ignore("Not testing this at this time")]
        public void TestSiteSearchBySpecialty()
        {
            Console.WriteLine("TestName: " + TestContext.CurrentContext.Test.MethodName.ToString());
            string searchTerm = "sports";
            _pgNMHomePage.GlobalSiteSearchEnterTerm(searchTerm);

        }

        /*
        [Test, Order(7)]
        [Category("BVT - Home Page Tests")]
        public void TestMobileHomePageGlobalSearch()
        {
            if (_browserType == BrowserType.Mobile)
            {
                _pgNMHomePage.MobileGlobalSiteSearchEnterTerm("sports");
            } 
            
        }
        */
    }
}
