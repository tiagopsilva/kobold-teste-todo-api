using Kobold.TodoApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Kobold.TodoApp.Api.Filters
{
    public class ModelValidatorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ErrorViewModel(
                    statusCode: HttpStatusCode.BadRequest,
                    message: "Valores inválidos para a requisição!",
                    data: new SerializableError(context.ModelState)));
            }
        }
    }
}
