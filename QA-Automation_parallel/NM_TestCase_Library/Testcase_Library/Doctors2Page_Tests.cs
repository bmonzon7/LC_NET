using System;
using Browserstack.NM_TestCase_Library.Pages;
using NUnit.Framework;
using FluentAssertions;
using static Browserstack.BrowserFactory;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium;
using NUnit.Framework.Internal;

namespace Browserstack.NM_TestCase_Library.Tescase_Library
{
    [TestFixture]
    public class Doctors2Page_Tests : BaseTestFixture
    {
        private string _urlNMDoctors2 = "http://nmcontent.laughlinreview.com/doctors"; //"https://uat.nm.org/doctors"; 
        public static Doctors2Page _pgDoctors2Page;

        [SetUp]
        public void Init()
        {
            PageInit();
        }

        [TearDown]
        public void Dispose()
        {
            _driver.Quit();

        }

        public Doctors2Page PageInit()
        {
            _browserType = BrowserType.Mobile;
            _browser = Browser.Chrome;

            try
            {
                _driver = BrowserFactory.StartBrowser(_browser, _browserType, _urlNMDoctors2);
                _pgDoctors2Page = new Doctors2Page(_driver);

                return _pgDoctors2Page;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
                Assert.Fail("Could Not Instanciate the WebDriver object");
                Console.WriteLine("Exception: " + e);
                return null;
            }
        }

        [Test]
        [Category("_Doctor Search")]
        [Ignore("Not for 7/17/20 demo")]
        public void TestDoctors2SearchByDoc_FullName()
        {
            Console.WriteLine("TestName: " + TestContext.CurrentContext.Test.MethodName.ToString());
            string strDoctorFullName = "karl bilimoria";
            //Doctors2Page doctors2Page = new Doctors2Page(_driver);
            var expectedURL = "https://uat12.nm.org/doctors2/1427186774/karl-y-bilimoria-md";
            Assert.AreEqual(_pgDoctors2Page.Doctors2SearchByDoctor_FullName(strDoctorFullName), expectedURL);
        }

        [Test]
        [Category("_Doctor Search")]
        [Ignore("Not for 7/17/20 demo")]
        public void TestDoctors2SearchByDoc_3CharInput()
        {
            Console.WriteLine("TestName: " + TestContext.CurrentContext.Test.MethodName.ToString());
            string str3Characters = "bil";
            _pgDoctors2Page.Doctors2SearchByDoctor_3CharInput(str3Characters);
        }

        [Test]
        [Category("BVT - Doctor Search")]
        [Obsolete]
        public void TestDoctors2SearchByDoc_2CharInput()
        {
            Console.WriteLine("TestName: " + TestContext.CurrentContext.Test.MethodName.ToString());

            string strSearchTerm = "ol";

            List<IWebElement> lstDoctorsResults = _pgDoctors2Page.Doctors2SearchByDoctor_2CharInput(strSearchTerm);
            Dictionary<string, List<KeyValuePair<string, string>>> excelExpectedResults;
            excelExpectedResults = _pgDoctors2Page.GetExpectedValuesXLS();
            List<string> lstDoctorsExpectedNameList = new List<string>();

            string strActualResultText;
            string[] lstDoctors;
            foreach (KeyValuePair<string, List<KeyValuePair<string, string>>> kvp in excelExpectedResults)
            {

                List<KeyValuePair<string, string>> valueList = new List<KeyValuePair<string, string>>();
                valueList = kvp.Value;
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    if (valueList[i].Key.ToString() == "ExpectedText")
                    {
                        string expectedText = valueList[i].Value.ToString();
                        lstDoctorsExpectedNameList.Add(expectedText);
                    }
                }
            }

            foreach (IWebElement webElement in lstDoctorsResults)
            {

                strActualResultText = webElement.Text;
                lstDoctors = strActualResultText.Split("\r\n");
                for (int i = 0; i < lstDoctors.Length; i++)
                {
                    string strDoctorActualName = lstDoctors[i].Trim();
                    Console.WriteLine("Actual Doctor Name: " + strDoctorActualName);
                    string strDoctorExpectedName = lstDoctorsExpectedNameList[i].Trim();
                    Console.WriteLine("Expected Doctor Name: " + strDoctorExpectedName);
                    strDoctorActualName.Should().Be(strDoctorExpectedName, "Doctors Name and Title should match Expected Results");

                }

            }
        }


    }
}


