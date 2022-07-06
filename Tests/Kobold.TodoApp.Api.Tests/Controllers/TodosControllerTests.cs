using Kobold.TodoApp.Api.Models;
using Kobold.TodoApp.Api.Models.Todos;
using Kobold.TodoApp.Api.Tests.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kobold.TodoApp.Api.Tests.Controllers
{
    public class TodosControllerTests : BaseSelfHostedTests
    {
        public TodosControllerTests(WebApplicationFactory<Startup> factory)
            : base(factory) { }

        [Fact]
        public async Task MetodoGetSemParametroDeveRetornarListaDeTodos()
        {
            // Arrange
            using var client = GetNewRestClient();
            await CreateTodos(client);

            // Act
            var todos = await client.GetJsonAsync<Todo[]>("/todos");

            // Assert
            Assert.True(todos.Length > 0);
        }

        [Fact]
        public async Task MetodoGetComParametroIdDeveRetornarTodoRelacionado()
        {
            // Arrange
            const int firstId = 1;
            using var client = GetNewRestClient();
            await CreateTodos(client);

            // Act
            var todo = await client.GetJsonAsync<Todo>($"/todos/{firstId}");

            // Assert
            Assert.NotNull(todo);
            Assert.Equal(firstId, todo.Id);
        }

        [Fact]
        public async Task MetodoCreateDeveCriarTodo()
        {
            // Arrange
            using var client = GetNewRestClient();
            await CreateTodos(client);
            var todovm = new TodoViewModel(true, "Outra tarefa para executar", 1);

            // Act
            var todo = await client.PostJsonAsync<TodoViewModel, Todo>($"/todos", todovm);

            // Assert
            Assert.NotNull(todo);
            Assert.NotNull(todo.Group);
            Assert.Equal(todovm.Done, todo.Done);
            Assert.Equal(todovm.GroupId, todo.Group.Id);
        }

        [Fact]
        public async Task DadoModeloComDadosInvalidosDeveRetornarStatusCodeBadRequestComMensagensDeFalha()
        {
            // Arrange
            using var client = GetNewRestClient();
            await CreateTodos(client);

            var todovm = new TodoViewModel(true, "x", 1);
            using var httpClient = _factory.CreateClient();
            using var content = new StringContent(JsonConvert.SerializeObject(todovm), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("/todos", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var error = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(error);
            Assert.NotNull(error.Data);

            var errors = JsonConvert.DeserializeObject<SerializableError>(error.Data.ToString());
            Assert.NotNull(errors);
            Assert.Single(errors);
            Assert.True(errors.ContainsKey(nameof(TodoViewModel.Description)));
        }

        [Fact]
        public async Task DadoIdDeRegistroInexistenteDeveRetornarMensagemFormatadaNotFound()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/todos/{int.MaxValue}");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            var error = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(error);
            Assert.Null(error.Data);
        }

        [Fact]
        public async Task MetodoUpdateDeveAlterarTodo()
        {
            // Arrange
            const int secondId = 2;
            using var client = GetNewRestClient();
            await CreateTodos(client);
            var todovm = new TodoViewModel(true, "Outra tarefa para executar", 1);

            // Act
            var todo = await client.PutJsonAsync<TodoViewModel, Todo>($"/todos/{secondId}", todovm);

            // Assert
            Assert.NotNull(todo);
            Assert.NotNull(todo.Group);
            Assert.Equal(todovm.Done, todo.Done);
            Assert.Equal(todovm.GroupId, todo.Group.Id);
        }

        [Fact]
        public async Task MetodoRemoveDeveRemoverItemDaLista()
        {
            // Arrange
            const int thirdId = 3;
            using var client = GetNewRestClient();
            await CreateTodos(client);

            // Act
            var response = await client.DeleteAsync(new RestRequest($"/todos/{thirdId}"));

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task CreateTodos(RestClient client)
        {
            var todos = new List<TodoWithGroupViewModel>
            {
                new TodoWithGroupViewModel(
                    description: "Criar recurso de Grupos (como tags) para classificar Todos", 
                    group: "Parte 1", 
                    done: true),
                new TodoWithGroupViewModel(
                    description: "Adicionar Testes",
                    group: "Parte 1 - Adicional",
                    done: true),
                new TodoWithGroupViewModel(
                    description : "Sincronizar remoção de Grupo e Todo entre os repositórios",
                    group: "Parte 1 - Adicional",
                    done: true),
                new TodoWithGroupViewModel(
                    description : "Adicionar Testes para Todos",
                    group: "Parte 1 - Adicional",
                    done: true),
                new TodoWithGroupViewModel(
                    description : "Adiconar tratamento global de exceptions não tratadas",
                    group: "Parte 2",
                    done: true),
                new TodoWithGroupViewModel(
                    description: "Adicionar validações",
                    group: "Parte 2 - Adicional",
                    done: true),
                new TodoWithGroupViewModel(
                    description: "Adicionar mapeamento das ViewModel para as Entidades",
                    group: "Parte 2 - Adicional",
                    done: true),
                new TodoWithGroupViewModel(
                    description: "Padronizar respostas",
                    group: "Parte 2 - Adicional",
                    done: true),
                new TodoWithGroupViewModel(
                    description: "Adicionar documentação OpenAPI",
                    group: "Parte 2 - Adicional",
                    done: true),
                new TodoWithGroupViewModel(
                    description: "Refinar documentação OpenAPI",
                    group: "Parte 2 - Adicional",
                    done: true),
                new TodoWithGroupViewModel(
                    description: "Atualizar README",
                    group: "Parte 2",
                    done: false),
            };

            await Task.WhenAll(todos.Select(async todo => await client.PostJsonAsync("/todos/with-group", todo)));
        }
    }
}
