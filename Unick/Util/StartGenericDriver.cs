using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
//using Unickq.SpecFlow.Selenium;


namespace Unick.Util
{

    [TestFixture]
    [Binding]
    public class StartGenericDriver
    {
       
        //public IWebDriver Browser { get; set; }

        protected  IWebDriver Browser;

        [BeforeScenario]
        public void Setup()
        {

        }
        [AfterScenario]
        public void TearDown()
        {
            //Dispose();
        }

        [AfterStep]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                //Browser = ScenarioContext.Current.GetWebDriver();
                Helpers.TearDown.TakeScreenshot(Browser);
                
            }


        }
       
    }
}