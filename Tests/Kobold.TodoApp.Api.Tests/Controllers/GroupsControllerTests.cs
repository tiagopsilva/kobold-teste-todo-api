using Kobold.TodoApp.Api.Models.Groups;
using Kobold.TodoApp.Api.Tests.Shared;
using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Kobold.TodoApp.Api.Tests.Controllers
{
    public class GroupsControllerTests : BaseSelfHostedTests
    {
        public GroupsControllerTests(WebApplicationFactory<Startup> factory)
            : base(factory) { }

        [Fact]
        public async Task MetodoGetSemParametroDeveRetornarListaDeGroups()
        {
            // Arrange
            using var client = GetNewRestClient();
            await CreateGroups(client);

            // Act
            var groups = await client.GetJsonAsync<Group[]>("/groups");

            // Assert
            Assert.True(groups.Length > 0);
        }

        [Fact]
        public async Task MetodoGetComParametroIdDeveRetornarGroupRelacionado()
        {
            // Arrange
            const int firstId = 1;
            using var client = GetNewRestClient();
            await CreateGroups(client);

            // Act
            var group = await client.GetJsonAsync<Group>($"/groups/{firstId}");

            // Assert
            Assert.NotNull(group);
            Assert.Equal(firstId, group.Id);
        }

        [Fact]
        public async Task DeveCadastrarGroupSemRepetirRegistros()
        {
            // Arrange
            using var client = GetNewRestClient();
            var groupvm = new GroupViewModel("A group");

            // Act
            var group1 = await client.PostJsonAsync<GroupViewModel, Group>("/groups", groupvm);
            var group2 = await client.PostJsonAsync<GroupViewModel, Group>("/groups", groupvm);

            // Assert                        
            Assert.NotNull(group1);
            Assert.NotNull(group2);
            Assert.True(group1.Id > 0);
            Assert.Equal(group1.Id, group2.Id);
            Assert.Equal(group1.Name, group2.Name);
            Assert.Equal(groupvm.Name, group1.Name);
            Assert.Equal(groupvm.Name, group2.Name);
        }

        [Fact]
        public async Task MetodoUpdateDeveAlterarGroup()
        {
            // Arrange
            const int secondId = 2;
            using var client = GetNewRestClient();
            await CreateGroups(client);
            var groupvm = new GroupViewModel("Group Changed");

            // Act
            var group = await client.PutJsonAsync<GroupViewModel, Group>($"/groups/{secondId}", groupvm);

            // Assert
            Assert.NotNull(group);
            Assert.Equal(groupvm.Name, group.Name);
        }

        [Fact]
        public async Task MetodoRemoveDeveRemoverItemDaLista()
        {
            // Arrange
            const int thirdId = 3;
            using var client = GetNewRestClient();
            await CreateGroups(client);

            // Act
            var response = await client.DeleteAsync(new RestRequest($"/groups/{thirdId}"));

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task CreateGroups(RestClient client)
        {
            var groups = new List<GroupViewModel>
            {
                new GroupViewModel("A group"),
                new GroupViewModel("Backlog"),
                new GroupViewModel("To do"),
                new GroupViewModel("In progress"),
                new GroupViewModel("Pending review"),
                new GroupViewModel("Done")
            };

            await Task.WhenAll(groups.Select(async group => await client.PostJsonAsync("/groups", group)));
        }
    }
}
