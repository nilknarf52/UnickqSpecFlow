﻿using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Unick;
using Unickq.SpecFlow.Selenium;

namespace Google
{

    [Binding]

    public class TesteSteps : Steps
    {
        protected readonly ScenarioContext ScenarioContext;
        protected readonly IWebDriver Browser;
        private Page Formulario;



        [Given(@"que eu esteja no site jobmidia\.com\.br")]
        public void DadoQueEuEstejaNoSiteJobmidia_Com_Br()
        {
            Formulario = new Page(Browser);
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


    }
}