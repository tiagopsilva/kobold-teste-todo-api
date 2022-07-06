using Kobold.TodoApp.Api.Models;
using Kobold.TodoApp.Api.Tests.Shared;
using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using System.Threading.Tasks;
using Xunit;

namespace Kobold.TodoApp.Api.Tests.Controllers
{
    public class AboutControllerTests : BaseSelfHostedTests
    {
        public AboutControllerTests(WebApplicationFactory<Startup> factory)
            : base(factory) { }

        [Fact]
        public async Task DeveRetornarNomeDaAplicacaoEVersao()
        {
            // Arrange
            using var client = GetNewRestClient();

            // Act
            var about = await client.GetJsonAsync<AboutViewModel>("/about");

            // Asset
            Assert.Equal("Kobold.TodoApp.Api", about.Nome);
            Assert.Equal("1.0.0", about.Versao);
        }
    }
}
