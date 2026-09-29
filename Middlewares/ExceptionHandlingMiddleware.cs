using System.Net;
using System.Text.Json;

namespace DeskFlowAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private RequestDelegate _next;
        
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception.Message.Contains("não encontrado", StringComparison.OrdinalIgnoreCase) ? HttpStatusCode.NotFound : HttpStatusCode.BadRequest;

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                status = (int)statusCode,
                mensagem = exception.Message,
                data = DateTime.UtcNow
            };
            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}