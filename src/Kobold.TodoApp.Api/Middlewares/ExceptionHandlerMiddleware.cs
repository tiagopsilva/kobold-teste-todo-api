using Kobold.TodoApp.Api.Helpers;
using Kobold.TodoApp.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Kobold.TodoApp.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly JsonHelperSerializer _jsonHelperSerializer;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, JsonHelperSerializer jsonHelperSerializer)
        {
            _next = next;
            _logger = logger;
            _jsonHelperSerializer = jsonHelperSerializer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound && !context.Response.HasStarted)
                    await CreateNotFoundResponse(context);
            }
            catch (Exception e)
            {
                LogException(context, e);
                await HandleException(context);
            }
        }

        private async Task CreateNotFoundResponse(HttpContext context, string customMessage = null)
        {
            context.Response.ContentType = "application/json";
            var json = _jsonHelperSerializer.SerializeObject(new ErrorViewModel(context.Response.StatusCode, customMessage));
            await context.Response.WriteAsync(json);
        }

        private void LogException(HttpContext context, Exception e)
        {
            var traceId = context.TraceIdentifier;
            var host = context.Request.Host;
            var path = context.Request.Path;
            var query = context.Request.QueryString;
            _logger.LogError(e, $"Exceção não tratada ocorrida em {host}{path}{query}, trace Id: {traceId}");
        }

        private async Task HandleException(HttpContext context, string customMessage = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new ErrorViewModel(
                context.Response.StatusCode, 
                customMessage ?? "Desculpe! Tivemos um problema mas nossa equipe já foi avisada e estamos atuando para resolvê-lo");

            var json = _jsonHelperSerializer.SerializeObject(error);

            await context.Response.WriteAsync(json);
        }
    }
}
