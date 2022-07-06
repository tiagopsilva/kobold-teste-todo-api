using Kobold.TodoApp.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kobold.TodoApp.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AboutController : ControllerBase
    {
        private readonly ILogger<AboutController> _logger;

        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns About information of API.
        /// </summary>
        /// <returns>About information</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /about
        ///
        /// </remarks>
        /// <response code="200">Returns the About information</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AboutViewModel> Get()
        {
            return Ok(new AboutViewModel
            {
                Nome = "Kobold.TodoApp.Api",
                Versao = "1.0.0"
            });
        }
    }
}
