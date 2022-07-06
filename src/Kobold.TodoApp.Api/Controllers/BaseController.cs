using Kobold.TodoApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kobold.TodoApp.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult<T> ResultOk<T>(T data)
        {
            return data != null
                ? Ok(data)
                : NotFoundError();
        }

        protected ActionResult ResultNoContent(bool success)
        {
            return success
                ? NoContent()
                : NotFoundError();
        }

        protected ActionResult NotFoundError()
        {
            return NotFound(new ErrorViewModel(HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString()));
        }
    }
}
