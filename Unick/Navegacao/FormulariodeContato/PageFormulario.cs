using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
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

        public void Navegacao()
        {
            SetupBrowser();
            _driver.Navigate().GoToUrl(Base);
        }

        public void PreenchimentoForm(Table table)

        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var formulario = table.CreateInstance<Formulario>();

            Actions actions = new Actions(_driver);
            actions.MoveToElement(camponome);
            actions.Click(camponome);
            actions.Build().Perform();

            var nome = wait.Until(ExpectedConditions.ElementExists(By.Id("name")));
            nome.SendKeys(formulario.Nome);

            var email = wait.Until(ExpectedConditions.ElementExists(By.Id("email")));
            email.SendKeys(formulario.Email);

            var telefone = wait.Until(ExpectedConditions.ElementExists(By.Id("phone")));
            telefone.SendKeys(formulario.Telefone);

            var mensagem = wait.Until(ExpectedConditions.ElementExists(By.Id("message")));
            mensagem.SendKeys(formulario.Mensagem);

        }

        public void ContatoMenu()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var contato = wait.Until(ExpectedConditions.ElementToBeClickable(contatomenu));
            contato.Click();
        }

        public void BotaoEnviar()
        {

            Actions actions = new Actions(_driver);
            actions.MoveToElement(camponome);
            actions.Click();
            actions.Build().Perform();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var enviar = wait.Until(ExpectedConditions.ElementExists(By.Id("enviarbotao")));
            enviar.Click();

        }

        public void ResultadoCorreto(string assert)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var results = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='success']/div/strong")));
            Assert.AreEqual(assert, results.Text);
        }


        public void ResultadoEmailIncorreto(string assert)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var results = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("p.help-block.text-danger > ul > li")));
            Assert.AreEqual(assert, results.Text);
        }
    }
}
