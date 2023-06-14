using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.Response;
using System.Text.Json;

namespace KwiatkiBeatkiAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                _logger.LogError($"Exeption message: [{badRequestException.Message}] | Inner exception message: [{badRequestException.InnerException?.Message ?? string.Empty}]" );
                await ProcessException(context: context, exception: badRequestException, responseMessage: "Error was occured", statusCode: StatusCodes.Status400BadRequest);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError($"Exeption message: [{notFoundException.Message}] | Inner exception message: [{notFoundException.InnerException?.Message ?? string.Empty}]");
                await ProcessException(context: context, exception: notFoundException, responseMessage: "Error was occured", statusCode: StatusCodes.Status404NotFound);
            }
            catch (UnprocessableContentException unprocessableContentException)
            {
                _logger.LogError($"Exeption message: [{unprocessableContentException.Message}] | Inner exception message: [{unprocessableContentException.InnerException?.Message ?? string.Empty}]");
                await ProcessException(context: context, exception: unprocessableContentException, responseMessage: "Error was occured", statusCode: StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await ProcessException(context: context, exception: ex, responseMessage: "Error was occured", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        private ValidationErrorResponse CreateErrorResponse(string responseMessage, Exception? exception = null)
        {
            var validationErrors = new Dictionary<string, string[]>
                {
                    { 
                        exception?.GetType().GetProperty("Code")?.GetValue(exception)?.ToString() ?? "Internal error", 
                        new[] { exception?.Message ?? "Nieznany błąd, zgłoś to do administratora aplikacji" } 
                    }
                };

            var response = new ValidationErrorResponse
            {
                Message = responseMessage,
                Errors = validationErrors
            };

            return response;
        }

        private async Task ProcessException(HttpContext context, Exception exception, string responseMessage, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            ValidationErrorResponse validationErrorResponse = CreateErrorResponse(responseMessage, exception);

            await context.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse));
        }
    }
}
