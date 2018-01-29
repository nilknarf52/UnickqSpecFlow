using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Unick;
using Unickq.SpecFlow.Selenium;

namespace Unick.Navegacao.FormulariodeContato
{

    [Binding]

    public class TesteSteps : Steps
    {
        protected readonly ScenarioContext ScenarioContext;
        protected readonly IWebDriver Browser;
        private PageFormulario Formulario;



        [Given(@"que eu esteja no site jobmidia\.com\.br")]
        public void DadoQueEuEstejaNoSiteJobmidia_Com_Br()
        {
            Formulario = new PageFormulario(Browser);
            Formulario.Navegacao();
        }

        [When(@"eu navegar até a área do formulário de contato")]
        public void QuandoEuNavegarAteAAreaDoFormularioDeContato()
        {
            Formulario.ContatoMenu();
        }

        [When(@"informo todos os dados corretamente")]
        public void QuandoInformoTodosOsDadosCorretamente(Table table)
        {
            Formulario.PreenchimentoForm(table);
        }

        [When(@"clico em Enviar")]
        public void QuandoClicoEmEnviar()
        {
            Formulario.BotaoEnviar();
        }

        [Then(@"o site ira informar a mensagem '(.*)'")]
        public void EntaoOSiteIraInformarAMensagem(string p0)
        {
            Formulario.ResultadoCorreto(p0);
        }


        public TesteSteps(ScenarioContext scenarioContext)
        {
           if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));
            ScenarioContext = scenarioContext;
            Browser = scenarioContext.GetWebDriver();
        
        }

        [When(@"entro em contato e informo todos os dados obrigatorios corretamente exceto email")]
        public void QuandoEntroEmContatoEInformoTodosOsDadosObrigatoriosCorretamenteExcetoEmail(Table table)
        {
            Formulario.PreenchimentoForm(table);
        }

        [Then(@"o formulario irá alertar o preenchimento incorreto do email '(.*)'")]
        public void EntaoOFormularioIraAlertarOPreenchimentoIncorretoDoEmail(string assert)
        {
            Formulario.ResultadoEmailIncorreto(assert);
        }


    }
}
