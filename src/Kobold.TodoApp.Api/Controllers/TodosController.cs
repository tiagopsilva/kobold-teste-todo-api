using Kobold.TodoApp.Api.Models;
using Kobold.TodoApp.Api.Models.Todos;
using Kobold.TodoApp.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Kobold.TodoApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : BaseController
    {
        private readonly ILogger<TodosController> _logger;
        private readonly ITodoService _todoService;

        public TodosController(ILogger<TodosController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        /// <summary>
        /// Returns a list of Todos.
        /// </summary>
        /// <returns>Todos</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /todos
        ///
        /// </remarks>
        /// <response code="200">Returns the Todos</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TodoResultViewModel>> Get()
        {
            return Ok(_todoService.Get());
        }

        /// <summary>
        /// Returns a list of Todos with Group.
        /// </summary>
        /// <returns>Todos with Group</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /todos/with-group
        ///
        /// </remarks>
        /// <response code="200">Returns the Todos with Group</response>
        [HttpGet("with-group")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TodoWithGroupResultViewModel>> GetWithGroup()
        {
            return Ok(_todoService.GetWithGroup());
        }

        /// <summary>
        /// Returns a Todo.
        /// </summary>
        /// <returns>A Todo</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /todos/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the Todo</response>
        /// <response code="404">If Todo not found by id</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorViewModel))]
        public ActionResult<TodoResultViewModel> Get([FromRoute] int id)
        {
            return ResultOk(_todoService.Get(id));
        }

        /// <summary>
        /// Creates a Todo.
        /// </summary>
        /// <returns>A newly created Todo</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /todos
        ///     {
        ///         "description": "A todo task",
        ///         "done": false
        ///         "groupId": 1 (optional)
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created Todo</response>
        /// <response code="400">If the Todo is null or invalid</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorViewModel))]
        public ActionResult<TodoWithGroupResultViewModel> Create([FromBody] TodoViewModel todovm)
        {
            var todo = _todoService.Create(todovm);
            return CreatedAtAction(nameof(Get), new { todo.Id }, todo);
        }

        /// <summary>
        /// Creates a Todo and a Group.
        /// </summary>
        /// <returns>A newly created Todo and Group</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /todos/with-group
        ///     {
        ///         "description": "A todo task",
        ///         "done": false
        ///         "group": (optional) { 
        ///             "name": "Backlog"
        ///         } 
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created Todo and Group</response>
        /// <response code="400">If the Todo is null or invalid</response>
        [HttpPost("with-group")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorViewModel))]
        public ActionResult<TodoWithGroupResultViewModel> Create([FromBody] TodoWithGroupViewModel todovm)
        {
            var todo = _todoService.Create(todovm);
            return CreatedAtAction(nameof(Get), new { todo.Id }, todo);
        }

        /// <summary>
        /// Creates a Todo.
        /// </summary>
        /// <returns>A Todo updated</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /todos/{id}
        ///     {
        ///         "done": false
        ///         "groupId": 2
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the updated Todo</response>
        /// <response code="400">If the Todo is null or invalid</response>
        /// <response code="404">If the Todo is not found by id</response>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorViewModel))]
        public ActionResult<TodoWithGroupResultViewModel> Update([FromRoute] int id, [FromBody] TodoUpdateViewModel todovm)
        {
            return ResultOk(_todoService.Update(id, todovm));
        }

        /// <summary>
        /// Delete a Todo.
        /// </summary>
        /// <returns>StatusCode 204 - No Content</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /todos/{id}
        ///
        /// </remarks>
        /// <response code="201">Returns the updated Todo</response>
        /// <response code="404">If the Todo is not found by id</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorViewModel))]
        public ActionResult Remove([FromRoute] int id)
        {
            return ResultNoContent(_todoService.Remove(id));
        }
    }
}
