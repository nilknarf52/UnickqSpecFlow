using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Unickq.SpecFlow.Selenium;

namespace Unick.Util
{
    public class GenericDriver
    {
        private IWebDriver Browser;
        protected  ScenarioContext ScenarioContext;
        public GenericDriver(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));
            ScenarioContext = scenarioContext;
            Browser = scenarioContext.GetWebDriver();
        }

        public void Dispose()
        {
            Browser.Close();
            Browser.Dispose();
            Browser.Quit();

        }
    }
}
