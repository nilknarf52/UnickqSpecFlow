using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Unick.Configuration;
using Unickq.SpecFlow.Selenium;


namespace Unick.Navegacao.FormulariodeContato
{
   
    public class PageFormulario : BaseUITest
    {


        public class Formulario
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string Mensagem { get; set; }

        }

        public string Base { get; set; }
        public PageFormulario(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(this._driver, this);
            Base = ConfigurationManager.AppSettings["base"].ToString();
        }

        [FindsBy(How = How.Id, Using = "contatomenu")]
        public IWebElement contatomenu { get; set; }

        [FindsBy(How = How.Id, Using = "name")]
        public IWebElement camponome { get; set; }

        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement email { get; set; }

        [FindsBy(How = How.Id, Using = "Phone")]
        public IWebElement telefone { get; set; }

        [FindsBy(How = How.Id, Using = "Message")]
        public IWebElement mensagem { get; set; }

        [FindsBy(How = How.Id, Using = "enviarbotao")]
        public IWebElement enviarbotao { get; set; }

        public void Navegacao()
        {
            SetupBrowser();
            _driver.Navigate().GoToUrl(Base);
        }
        private dynamic _instance;
        public void PreenchimentoForm(Table table)

        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var formulario = table.CreateInstance<Formulario>();
            //_instance = table.CreateDynamicInstance();


            var nome = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(camponome));
            nome.SendKeys(formulario.Nome);

            var email = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("email")));
            email.SendKeys(formulario.Email);

            var telefone = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("phone")));
            telefone.SendKeys(formulario.Telefone);

            var mensagem = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("message")));
            mensagem.SendKeys(formulario.Mensagem);

        }

        public void ContatoMenu()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var contato = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(contatomenu));
            contato.Click();
            Thread.Sleep(2000);
        }

        public void BotaoEnviar()
        {

            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", mensagem);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var enviar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("enviarbotao")));
            enviar.Click();
            Thread.Sleep(2000);

        }
        #region Assert
        public void ResultadoCorreto(string assert)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var results = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='success']/div/strong")));
            Assert.AreEqual(assert, results.Text);
        }

        public void ResultadoEmailIncorreto(string assert)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var results = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("p.help-block.text-danger > ul > li")));
            Assert.AreEqual(assert, results.Text);
        }

        public void ResultadoMensagem(string assert)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var results = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//div[@id='Message']//ul[@role='alert']/li[.='Por favor informe uma mensagem.']")));
            Assert.AreEqual(assert, results.Text);
        }
        public void ResultadoTelefone(string assert)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var results = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//div[@id='Phone']//ul[@role='alert']/li[.='Por favor informe seu telefone.']")));
            Assert.AreEqual(assert, results.Text);
        }
        #endregion



    }

}
