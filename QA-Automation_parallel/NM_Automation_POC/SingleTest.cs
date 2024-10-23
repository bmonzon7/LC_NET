using NUnit.Framework;
using OpenQA.Selenium;
using BrowserStack;
using System;

namespace Automation_Framework

{
    [TestFixture("single", "chrome")]
  public class SingleTest : BrowserStackNUnitTest
  {
    public SingleTest(string profile, string environment) : base(profile, environment) { }

    [Test]
    public void SearchGoogle()
    {
            Console.WriteLine("Entered Single Fucntion");
      driver.Navigate().GoToUrl("https://www.google.com/ncr");
      IWebElement query = driver.FindElement(By.Name("q"));
      query.SendKeys("BrowserStack");
      query.Submit();
      System.Threading.Thread.Sleep(5000);
      Assert.AreEqual("BrowserStack - Google Search", driver.Title);
    }
  }
}
