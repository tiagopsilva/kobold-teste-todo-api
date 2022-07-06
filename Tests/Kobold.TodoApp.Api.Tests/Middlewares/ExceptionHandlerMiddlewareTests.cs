using Kobold.TodoApp.Api.Models;
using Kobold.TodoApp.Api.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Kobold.TodoApp.Api.Tests.Middlewares
{

    public class ExceptionHandlerMiddlewareTests
    {
        [Fact]
        public async Task DeveRetornarMensagemFormatadaQuandoOcorreUmaExceptionNaAplicacao()
        {
            // Arrange
            var app = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddScoped<IGroupService, GroupFakeService>();
                    });
                });

            var client = app.CreateClient();

            // Act
            var response = await client.GetAsync("/groups");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            var error = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(error);
            Assert.Equal(HttpStatusCode.InternalServerError, error.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundQuandoSolicitadoRecursoInexistente()
        {
            // Arrange
            var app = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });

            var client = app.CreateClient();

            // Act
            var response = await client.GetAsync($"/{Guid.NewGuid():N}");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
