using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Unick;
using Unickq.SpecFlow.Selenium;

namespace Unick.Navegacao.FormulariodeContato
{

    [Binding]

    public class BaseFormulario
    {
        protected readonly ScenarioContext scenarioContext;
        //private readonly ScenarioContext scenarioContext;

        protected readonly IWebDriver Browser;

        private PageFormulario Formulario;

        public BaseFormulario(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null) throw new ArgumentNullException("scenarioContext");
            this.scenarioContext = scenarioContext;
            Browser = scenarioContext.GetWebDriver();

        }
        
        [Given(@"que eu esteja no site jobmidia")]
        public void DadoQueEuEstejaNoSiteJobmidia_Com_Br()
        {
            Formulario = new PageFormulario(Browser);
            Formulario.Navegacao();
        }

        [Given(@"navego em formulario de contato")]
        public void QuandoEuNavegarAteAAreaDoFormularioDeContato()
        {
            Formulario.ContatoMenu();
        }

        [Given(@"informo todos os dados")]
        public void PreenchimentoFormulario(Table table)
        {
            Formulario.PreenchimentoForm(table);
        }

        [Given(@"informo o email incompleto")]
        public void EmailIncompleto(Table table)
        {
            Formulario.PreenchimentoForm(table);
        }
        [Given(@"não informo mensagem")]
        public void DadoNaoInformoMensagem(Table table)
        {
            Formulario.PreenchimentoForm(table);
        }


        [Given(@"não informo telefone")]
        public void DadoNaoInformoTelefone(Table table)
        {
            Formulario.PreenchimentoForm(table);
        }

        [When(@"envio o formulario")]
        public void EnviarFormulario()
        {
            Formulario.BotaoEnviar();
        }

        [Then(@"recebo a mensagem de sucesso '(.*)'")]
        public void MensagemDeSucesso(string assert)
        {
            Formulario.ResultadoCorreto(assert);
        }


        [Then(@"recebo a mensagem de validação do e-mail '(.*)'")]
        public void ValidacaoEmail(string assert)
        {
            Formulario.ResultadoEmailIncorreto(assert);
        }
        [Then(@"recebo a mensagem de validação de mensagem '(.*)'")]
        public void ValidacaoMensagem(string assert)
        {
            Formulario.ResultadoMensagem(assert);
        }


        [Then(@"recebo a mensagem de validação de telefone '(.*)'")]
        public void EntaoReceboAMensagemDeValidacaoDeTelefone(string assert)
        {
            Formulario.ResultadoTelefone(assert);
        }

    }
}
