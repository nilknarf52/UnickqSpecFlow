#language: pt-BR
Funcionalidade: Formulario de Contato
	Eu como cliente, preciso contactar o prestador de serviço
	Para que seja necessário um possível retorno
	Preciso preencher as informações do formulário corretamente.

Contexto: 
	Dado que eu esteja no site jobmidia
	E sou direcionado para a página principal
	E navego em formulario de contato

@Browser:FirefoxDocker @Browser:IEDocker @Browser:IE @Browser:ChromeDocker @Browser:Edge
Cenário: Preenchimento do formulario corretamente
	E informo todos os dados
	 |Nome		|Email					|Telefone	  | Mensagem		  |
	 |Franklin	|franklinjob@hotmail.com|(21)991475281|	Teste de Mensagem |
	Quando envio o formulario
	Então recebo a mensagem de sucesso 'Sua mensagem foi enviada com sucesso.'

@Browser:PhantomDocker @negativo @Browser:IE
Cenário: Preenchimento do formulario com email incorreto
	Mas informo o email incompleto
	|Nome		|Email					|Telefone	  | Mensagem		  |
    |Franklin	|franklinjob@           |(21)991475281|	Teste de Mensagem |
	Quando envio o formulario
	Então recebo a mensagem de validação do e-mail 'Formato de e-mail inválido'

@Browser:IE @Browser:Chrome @Browser:Firefox @negativo
Cenário: Preenchimento do formulario sem mensagem
	Mas não informo mensagem
	|Nome		|Email					|Telefone	  | Mensagem		  |
    |Franklin	|franklinjob@hotmail.com|(21)991475281|					  |
	Quando envio o formulario
	Então recebo a mensagem de validação de mensagem 'Por favor informe uma mensagem.'

@Browser:IE @Close
Cenário: Preenchimento do formulario telefone incorreto
	Mas não informo telefone
	|Nome		|Email					|Telefone	  | Mensagem		  |
    |Franklin	|franklinjob@hotmail.com|			  |	Teste de Mensagem |
	Quando envio o formulario
	Então recebo a mensagem de validação de telefone 'Por favor informe seu telefone.'