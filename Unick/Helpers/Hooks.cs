using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using BoDi;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Unickq.SpecFlow.Selenium;

namespace Unick.Helpers
{
    [Binding]
    public class Hooks : GetScreenshot

    {
        //Global Variable for Extend report
        private static ExtentTest featureName;
        private static KlovReporter klov;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private readonly IObjectContainer _objectContainer;
        protected IWebDriver Browser;
        protected readonly ScenarioContext scenarioContext;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [AfterScenario("Close")]
        public void CloseWebDriver()
        {
            if (ScenarioContext.Current.TestError != null)
                Browser.Dispose();
        
                
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            //Initialize Extent report before test starts
            string PathTargetReport = ConfigurationManager.AppSettings["FilePathTarget"];
            var htmlReporter = new ExtentHtmlReporter(PathTargetReport + "Report.html");

            htmlReporter.Configuration().Theme = Theme.Dark;
            extent = new ExtentReports();
            //var klov = new KlovReporter();

            // specify mongoDb connection
            // klov.InitMongoDbConnection("localhost", 27017);

            // specify project ! you must specify a project, other a "Default project will be used"
            //klov.ProjectName = "Sistema de Apuração de Interatividade";

            // you must specify a reportName otherwise a default timestamp will be used
            // klov.ReportName = "Execução de Testes " + DateTime.Now.ToString();

            // URL of the KLOV server
            // klov.KlovUrl = "http://localhost";

            extent.AddSystemInfo("Selenium", "3.10.0");
            extent.AddSystemInfo("Specflow", "2.3.2");
            extent.AddSystemInfo("NUnit", "3.8.1");
            //Attach report to reporter
            //extent.AttachReporter(htmlReporter, klov);
            extent.AttachReporter(htmlReporter);


        }


        [BeforeFeature]

        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
            extent.Flush();
           
        }


        [AfterStep]
        public void InsertReportingSteps()
        {
            #region TakeScreenshot
            if (ScenarioContext.Current.TestError != null)
            {
                Browser = ScenarioContext.Current.GetWebDriver();
                TakeScreenshot(Browser);
            }

            #endregion

            #region InsertReportingSteps
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(ScenarioContext.Current, null);

            if (ScenarioContext.Current.TestError == null)
            {

                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                featureName.AddScreenCaptureFromPath(ScreenshotFilePath);


            }

            //Pending Status
            if (TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");

            }


            #endregion

        }



        [BeforeScenario]
        public void Initialize()
        {
            //Create dynamic scenario name
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            scenario.AssignCategory(ScenarioContext.Current.ScenarioInfo.Tags);

        }



    }
}
