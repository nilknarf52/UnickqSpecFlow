using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
//using Unickq.SpecFlow.Selenium;

namespace Unick.Config
{
    public class BaseUITest
    {
        protected IWebDriver _driver;

        public void SetupBrowser()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(45);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(50);
            _driver.Manage().Window.Maximize();
        }


    }
}

