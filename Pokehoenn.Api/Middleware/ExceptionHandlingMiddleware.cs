using MongoDB.Driver;
using System.Text.Json;

namespace Pokehoenn.Api.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private const string ErrorAccessingDb = "An error occurred while accesing the Database";
        private const string ErrorDbTimeout = "The request to the database timed out.";
        private const string ErrorInternalServer = "An unexpected error occurred.";

        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (MongoException e) 
            {
                _logger.LogError(e, "An error occurred while accessing MongoDB.");
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, ErrorAccessingDb);
            }
            catch (TimeoutException e)
            {
                _logger.LogError(e, "A timeout occurred while accessing MongoDB");
                await HandleExceptionAsync(context, StatusCodes.Status504GatewayTimeout, ErrorDbTimeout);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An unexpected error occurred.");
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, ErrorInternalServer);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new { error = message });
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
