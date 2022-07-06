using System.Net;

namespace Kobold.TodoApp.Api.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(int statusCode, string message, object data = null)
        {
            StatusCode = (HttpStatusCode)statusCode;
            Message = message ?? StatusCode.ToString();
            Data = data;
        }

        public ErrorViewModel(HttpStatusCode statusCode, string message, object data = null)
        {
            StatusCode = statusCode;
            Message = message ?? StatusCode.ToString();
            Data = data;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
