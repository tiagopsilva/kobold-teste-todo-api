using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using Xunit;

namespace Kobold.TodoApp.Api.Tests.Shared
{
    public abstract class BaseSelfHostedTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> _factory;

        protected BaseSelfHostedTests(WebApplicationFactory<Startup> factory)
            => _factory = factory;

        protected RestClient GetNewRestClient()
            => new RestClient(_factory.CreateClient(), true);
    }
}
