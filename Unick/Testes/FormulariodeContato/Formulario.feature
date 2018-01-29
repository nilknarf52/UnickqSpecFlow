#language: pt-BR
Funcionalidade: Formulario de Contato
	Eu como usuário, preciso contactar o prestador de serviço
	Para que seja necessário um possível retorno
	Preciso preencher as informações corretamente.

Contexto: 
	Dado que eu esteja no site jobmidia.com.br
	Quando eu navegar até a área do formulário de contato

@Browser:Chrome @positivo
Cenario: Preenchimento do formulario corretamente
	Quando informo todos os dados corretamente
	 |Nome		|Email					|Telefone	  | Mensagem		  |
	 |Franklin	|franklinjob@hotmail.com|(21)991475281|	Teste de Mensagem |
	E clico em Enviar
	Entao o site ira informar a mensagem 'Sua mensagem foi enviada com sucesso.'


	
@Browser:Chrome @negativo
	Cenario: Preenchimento do formulario com email incorreto
	Quando entro em contato e informo todos os dados obrigatorios corretamente exceto email
	|Nome		|Email					|Telefone	  | Mensagem		  |
    |Franklin	|franklinjob@           |(21)991475281|	Teste de Mensagem |
	Entao o formulario irá alertar o preenchimento incorreto do email 'Formato de e-mail inválido'