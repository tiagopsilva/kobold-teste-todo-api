using Kobold.TodoApp.Api.Models;
using Kobold.TodoApp.Api.Models.Groups;
using Kobold.TodoApp.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Kobold.TodoApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : BaseController
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _groupService;

        public GroupsController(ILogger<GroupsController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        /// <summary>
        /// Returns a list of Groups.
        /// </summary>
        /// <returns>Groups</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /groups
        ///
        /// </remarks>
        /// <response code="200">Returns the Groups</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<GroupResultViewModel>> Get()
        {
            return Ok(_groupService.Get());
        }

        /// <summary>
        /// Returns a list of Groups with Todos.
        /// </summary>
        /// <returns>Groups with Todos</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /groups/with-todos
        ///
        /// </remarks>
        /// <response code="200">Returns the Groups with Todos</response>
        [HttpGet("with-todos")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<GroupWithTodosResultViewModel>> GetWithTodos()
        {
            return Ok(_groupService.GetWithTodos());
        }

        /// <summary>
        /// Returns a Group.
        /// </summary>
        /// <returns>A Group</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /groups/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the Group</response>
        /// <response code="404">If the Group not found by the id</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorViewModel))]
        public ActionResult<GroupResultViewModel> Get(int id)
        {
            return ResultOk(_groupService.Get(id));
        }

        /// <summary>
        /// Returns a Group with Todos.
        /// </summary>
        /// <returns>A Group with Todos</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /groups/{id}/with-todos
        ///
        /// </remarks>
        /// <response code="200">Returns the Group with Todos</response>
        /// <response code="404">If the Group not found by the id</response>
        [HttpGet("{id}/with-todos")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorViewModel))]
        public ActionResult<GroupWithTodosResultViewModel> GetWithTodos(int id)
        {
            return ResultOk(_groupService.GetWithTodos(id));
        }

        /// <summary>
        /// Creates a Group.
        /// </summary>
        /// <returns>A newly created Group</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /groups
        ///     {
        ///        "name": "Backlog"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created Group</response>
        /// <response code="400">If the Group is null or invalid</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorViewModel))]
        public ActionResult<GroupResultViewModel> Create([FromBody] GroupViewModel groupvm)
        {
            var group = _groupService.Create(groupvm);
            return CreatedAtAction(nameof(Get), new { group.Id }, group);
        }

        /// <summary>
        /// Update a Group.
        /// </summary>
        /// <returns>A Group updated</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /groups/{id}
        ///     {
        ///        "name": "Backlog"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the Group updated</response>
        /// <response code="400">If the item is null or invalid</response>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorViewModel))]
        public ActionResult<GroupResultViewModel> Update([FromRoute] int id, [FromBody] GroupViewModel groupvm)
        {
            return ResultOk(_groupService.Update(id, groupvm));
        }

        /// <summary>
        /// Delete a Group.
        /// </summary>
        /// <returns>StatusCode 204 - No Content</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /groups/{id}
        ///
        /// </remarks>
        /// <response code="204">StatusCode 204 (No Content)</response>
        /// <response code="404">If the Group not found by the id</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorViewModel))]
        public ActionResult Remove([FromRoute] int id)
        {
            return ResultNoContent(_groupService.Remove(id));
        }
    }
}
