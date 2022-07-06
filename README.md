# Kobold.TodoApp

## Introdução

Este repositório define uma web API para gerenciamento de tarefas. O código fonte está organizado da seguinte forma:

* Controllers:
* Models:
* Services:
* run.sh: arquivo de inicialização da aplicação.

Para iniciar a aplicação execute o comando:

```
> dotnet run -p src/Kobold.TodoApp.Api
```

## Exercício

O exercício é composto de duas partes, a primeira obrigatória e a segunda opcional.

### Parte I

Altere o código da aplicação, implementando um mecanismo de agrupamento das tarefas (definidas pela classe `Todo`) em coleções, expondo as novas funcionalidades implementadas para consumo na web API.

### Parte II

A aplicação não define um mecanismo de tratamento de erros, e exceções no processamento são expostas na resposta ao usuário. Implementar mecanismo de tratamento de erros na aplicação, de forma que as respostas apresentadas ao usuário não exponham detalhes da aplicação, e apresentem mensagens claras.

## Avaliação

No processo de avaliação do código, avaliaremos as seguintes características:
* Aderência da implementação ao código existente.
* Clareza do código implementado.
* Documentação das alterações efetuadas.
* Estrutura das mensagens de commit.

A solução para o problema não é única. O candidato deve analisar o código existente, definir as funcionalidades a serem implementadas e implementa-las. A avaliação da solução apresentada será realizada em conversa com o candidato, com o objetivo de entender o processo de análise e tomada de decisões que levou àquela solução.

## Documentação

Sobre a nova estrutura da aplicação após implementações solicitadas e itens adicionais implementados.

### Pacotes NuGet adicionados

A aplicação faz uso dos pacotes:
* **AutoMapper**: para mapeamento/conversão de classes de Entidade para ViewModels e vice-versa
* **NewtonsoftJson**: como alternativa para serialiação e desserialização das classses de/para o formato JSON, principalmente por conter opção para tratar referências cíclicas
* **Swashbuckle (Swagger)**: Para documentação Open API
* **xUnit**: Para testes
* **Microsoft.AspNetCore.Mvc.Testing**: para self-host e teste de integração
* **RestSharp: para facilitar** as requisições nos testes

### Classe abstrata BaseController

Classe para ser usada como superclasse dos controllers da aplicaão.  
Ela já herda de ControllerBase então nenhuma funcionlidade é perdida.  
Ela possui os métodos `ResultOk<T>()`, `ResultNoContent()` e `NotFoundError()` que podem ser utilizados para padronizar as respostas.

### Classe ExceptionHandlerMiddleare

A classe **ExceptionHandlerMiddleware** está configurada para capturar as exceções não tratadas na aplicação e entregar uma resposta padrão para o usuário, cumprindo os requisitos da Parte II do teste.

Além disso ela possui uma condição para tratar o formato da resposta para erros 404 que terão o formato JSON da classe **ErrorViewModel**, similar a:

```
{
    "statusCode": 404,
    "message": "NotFound",
    "data": { ... }
}
```
O mesmo formato é utilizado para respostas do tipo *BadRequest* como que para outros tipos de resposta.

### Demais padronizações de respostas

Os tipo retornados pelos controllers foram substituídos por classes do Tipo *ResultViewModel* para controlar o que é exposto para o cliente e readicionar controle na serialização das entidades evitando referências cíclicas.

Esses tipos retornados ainda foram encapsulados pela classe *ActionResult<>*, não perdendo assim referência ao tipo do retorno, o que aumenta a capacidade de mapeamento automático pelo Swagger, e permitindo a padronização de respostas mais próximas ao padrão RESTfull:
- 200 OK para métodos GET
- 201 Created para métodos POST
- 200 OK ou 204 No Content para métodos PUT
- 204 No Content para métodos DELETE
- 404 Not Found para quando o registro não é encontrado