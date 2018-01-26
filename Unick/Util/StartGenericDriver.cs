using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Unick.Util
{

    [TestFixture]

    public class StartGenericDriver : GenericDriver
    {

        [TearDown]


        [BeforeScenario]
        public void Setup()
        {

        }
        [AfterScenario]
        public void TearDown()
        {
            Dispose();
        }

        [AfterStep]
        public void AfterStep()
        {
            //if (ScenarioContext.Current.TestError != null)
            {

              Helpers.TearDown.TakeScreenshot(Instance);

            }


        }

    }
}