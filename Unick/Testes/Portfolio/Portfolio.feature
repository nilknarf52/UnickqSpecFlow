#language: pt-BR

Funcionalidade: Portfolio
	Eu como cliente, preciso acessar a área de portfolio do site para que seja possível 
	análisar os trabalhos realizados e efetuar um possível contato

Contexto: 
	Dado que eu esteja no site jobmidia
	E navego em portfolio

@mytag
Cenario: Análise do portfolio
	Quando clico na imagem do preview
	E é exibido o trabalho maximizado
	Quando clico em fechar
	Então retorno para a exibição do portfolio

