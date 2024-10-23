using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Browserstack.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
/// <summary>
/// Doctors2 Search Page - The page on NM is built with React
/// </summary>
namespace Browserstack.NM_TestCase_Library.Pages
{
    public class Doctors2Page : Page, IPageController
    {
        private readonly IWebDriver _driver;
        readonly string predictiveResults_2CharNameSearchXpath = "/html/body/main/div[2]/div/div/div/section/article/div[1]/div/div/div/div/div/div/div[1]/ul";
        readonly string searchInputFieldXPath = "/html/body/main/div[2]/div/div/div/section/article/div[1]/div/div/div/form/div/input";
        //readonly string mobileSearchInputFieldXPath = "/html/body/main/div[2]/div/div/div/section/article/div[1]/div/div/div/form/div/input";
        //readonly string mobilePredictiveResults_2CharNameSearchXpath = "/html/body/main/div[2]/div/div/div/section/article/div[1]/div/div/div/div/div/div/div[1]/ul";

        readonly string predictiveResults_3CharNameSearchXpath = "/html/body/main/div[2]/div/div/div/section/article/div[1]/div/div/div/div/div/div/div[1]/ul";
        readonly string predictiveResults_FullNameSearchXpath = "/html/body/main/div[2]/div/div/div/section/article/div[1]/div/div/div/div/div/div/div[1]";
        readonly string urlDoctors2PageURL = "http://nmcontent.laughlinreview.com/doctors";       //"https://uat.nm.org/doctors";

        protected IWebElement txtDoctorSearchInput => this._driver.FindElement(By.XPath(searchInputFieldXPath));
        // protected IWebElement mobileDoctorSearchInput => this._driver.FindElement(By.XPath(mobileSearchInputFieldXPath));

        public Doctors2Page(IWebDriver _driver) : base(_driver)
        {
            this._driver = _driver;

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(1);
            Thread.Sleep(12000);
        }

        public void GoToDoctors2Page()
        {
            _driver.Navigate().GoToUrl(urlDoctors2PageURL);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(10000);
            _driver.Manage().Window.Maximize();
            Thread.Sleep(12000);
        }

        public string Doctors2SearchByDoctor_FullName(string searchTerm)
        {
            txtDoctorSearchInput.Clear();
            string strSearchTerm = searchTerm;
            txtDoctorSearchInput.SendKeys(strSearchTerm);
            Thread.Sleep(30000);
            IWebElement doctorResults = _driver.FindElement(By.XPath(predictiveResults_FullNameSearchXpath));
            doctorResults.Click();
            Thread.Sleep(15000);
            TakeScreenshot();
            return _driver.Url;
            //var expectedURL = "https://uat12.nm.org/doctors2/1427186774/karl-y-bilimoria-md"; 
        }

        public void Doctors2SearchByDoctor_3CharInput(string docName)
        {
            txtDoctorSearchInput.Clear();
            string doctorName = docName;
            _driver.Manage().Window.Maximize();
            txtDoctorSearchInput.SendKeys(doctorName);
            Thread.Sleep(40000);
            List<IWebElement> doctorsResultList = new List<IWebElement>();

            doctorsResultList = this.FindElements(By.XPath(predictiveResults_3CharNameSearchXpath)).ToList();

            foreach (IWebElement webElement in doctorsResultList)
            {
                string tmp = webElement.Text;


                Console.WriteLine("Split with multiple separators");
                string[] doctorsList = tmp.Split("\r\n");
                foreach (string doctor in doctorsList)
                {
                    Console.WriteLine("Doctor Name: " + doctor);
                }

                // Console.WriteLine("The Value of the item on the list is: " + tmp);
            }

        }

        [Obsolete]
        public List<IWebElement> Doctors2SearchByDoctor_2CharInput(string docName)
        {
            var tmp = _driver.Url;

            string strDoctorName = docName;

            IJavaScriptExecutor je = (IJavaScriptExecutor)_driver;
            je.ExecuteScript("window.scrollTo(0, 200)");
            //je.ExecuteScript("window.scrollBy(0,150);");

            //Seleium WAITS
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until((d) => { return txtDoctorSearchInput; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(45);

            WebDriverWait waitToBeVisible = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));

            txtDoctorSearchInput.Click();
            txtDoctorSearchInput.SendKeys(strDoctorName);
            Thread.Sleep(30000);
            waitToBeVisible.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(predictiveResults_2CharNameSearchXpath)));



            List<IWebElement> listDoctorsResultList = new List<IWebElement>();
            listDoctorsResultList = this.FindElements(By.XPath(predictiveResults_2CharNameSearchXpath)).ToList();

            //Seleium WAITS
            WebDriverWait wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait2.Until((d) => { return listDoctorsResultList; });
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return listDoctorsResultList;
        }


        public override void TakeScreenshot()
        {
            string dtDate = DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
            string fileName = (@"c:\temp\screenshots\" + dtDate + ".png");
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
        }

        //We will use these methods to retrieve the expected values from db or Excel
        public override string GetExpectedPageTitle()
        {
            throw new NotImplementedException();
        }

        public override string GetExpectedUri()
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, List<KeyValuePair<string, string>>> GetExpectedValuesXLS()
        {
            string strFileName = @"C:\Users\bmonzon\source\repos\QA-Automation\NM_Automation_POC\Externals\TestDataDocSearch.xlsx";
            try
            {
                Excel_ReaderDocSearch excel_Reader = new Excel_ReaderDocSearch();
                Dictionary<string, List<KeyValuePair<string, string>>> dict = excel_Reader.ReadOutXLS(strFileName);
                return dict;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception trying to read from Excel -- Exception: " + e);
                return null;
            }
        }


    }
}
