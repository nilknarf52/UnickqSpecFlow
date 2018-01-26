using NUnit.Framework;
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
    [TestFixture]

    public class GenericDriver
    {
   
        protected readonly IWebDriver Browser;
        private IWebDriver _instance;
        public IWebDriver Instance
        {
            get
            {
                Wait.Until(t => { _instance = t; return t; });
                return _instance;
            }
        }

        private WebDriverWait wait;
        public WebDriverWait Wait
        {
            get
            {
                if (wait == null)
                    wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(2));

                return wait;
            }
        }


        public void Dispose()
        {
            Browser.Close();
            Browser.Dispose();
            Browser.Quit();

        }
    }
}
