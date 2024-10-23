using BrowserStack;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Linq;


namespace Automation_Framework
{
    [TestFixture]
  public class BrowserStackNUnitTest
  {
    protected IWebDriver driver;
    protected string profile;
    protected string environment;
    private Local browserStackLocal;
    public IConfiguration configuration { get; set; }
        

        public BrowserStackNUnitTest(string profile, string environment)
    {
      this.profile = profile;
      this.environment = environment;
    }
    
    [SetUp]
        [Obsolete]
        public void Init()
    {
            Console.WriteLine("Setup Entered!");


            var builder = new ConfigurationBuilder().
            SetBasePath(Directory.GetCurrentDirectory() + "../../../../").
            AddJsonFile("appsettings.json");
            configuration = builder.Build();

            Dictionary<string, string> capsec = new Dictionary<string, string>();

            capsec = configuration.GetSection("appSettings").GetChildren().ToDictionary(x => x.Key, x => x.Value as string);
            var counter = 0;
            List<string> cred = new List<string>();
            foreach (KeyValuePair<string, string> kvp in capsec)
            {
                cred.Add(kvp.Value);
                counter++;
                Console.WriteLine("{0},{1}", kvp.Key, kvp.Value);
            }

            var user = cred.ElementAt(2);
            var key = cred.ElementAt(0);
            var url = cred.ElementAt(1);

            //Console.WriteLine("user: " + user + "  Key: " + key);

            var caps = configuration["appSettings:environment"];

            capsec = configuration.GetSection("profile:" + profile).GetChildren().ToDictionary(x => x.Key, x => x.Value as string);
            DesiredCapabilities capabilities = new DesiredCapabilities();


            foreach (KeyValuePair<string, string> kvp in capsec)
            {

                capabilities.SetCapability(kvp.Key, kvp.Value);
                if (kvp.Key == "browserstack.local" && kvp.Value == "true")
                {
                    browserStackLocal = new Local();
                    List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
                        new KeyValuePair<string, string>("key", key )};
                    bsLocalArgs.Add(new KeyValuePair<string, string>("binarypath", "/Users/rathilpatel/Downloads/BrowserStackLocal"));
                    browserStackLocal.start(bsLocalArgs);
                    Thread.Sleep(5000);
                }
                Console.WriteLine("{0},{1}", kvp.Key, kvp.Value);
            }

            capsec = configuration.GetSection("environment:" + environment).GetChildren().ToDictionary(x => x.Key, x => x.Value as string);


            foreach (KeyValuePair<string, string> kvp in capsec)
            {

                capabilities.SetCapability(kvp.Key, kvp.Value);
                Console.WriteLine("{0},{1}", kvp.Key, kvp.Value);
            }


            driver = new RemoteWebDriver(
              new Uri("http://"+user+":"+key+"@"+url+"/wd/hub/"), capabilities);
        }

    [TearDown]
    public void Cleanup()
    {
      driver.Quit();
      if (browserStackLocal != null)
      {
        browserStackLocal.stop();
      }
    }
  }
}
