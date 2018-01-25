#language: pt-BR
Funcionalidade: Formulario de Contato
	Eu como usuário, preciso contactar o prestador de serviço
	Para que seja necessário um possível retorno
	Preciso preencher as informações corretamente.

Contexto: 
	Dado que eu esteja no site jobmidia.com.br
	Quando eu navegar até a área do formulário de contato

@Browser:Chrome
Cenario: Preenchimento do formulario corretamente
	Quando informo todos os dados corretamente
	 |Nome		|Email					|Telefone	  | Mensagem		  |
	 |Franklin	|franklinjob@hotmail.com|(21)991475281|	Teste de Mensagem |
	E clico em Enviar
	Entao o site ira informar a mensagem 'Sua mensagem foi enviada com sucesso.'
	

