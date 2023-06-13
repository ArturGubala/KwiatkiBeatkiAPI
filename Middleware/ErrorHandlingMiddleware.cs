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

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                ValidationErrorResponse validationErrorResponse = CreateErrorResponse("Error was occured", badRequestException);

                await context.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse));
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError($"Exeption message: [{notFoundException.Message}] | Inner exception message: [{notFoundException.InnerException?.Message ?? string.Empty}]");

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                ValidationErrorResponse validationErrorResponse = CreateErrorResponse("Error was occured", notFoundException);

                await context.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse));
            }
            catch (UnprocessableContentException unprocessableContentException)
            {
                _logger.LogError($"Exeption message: [{unprocessableContentException.Message}] | Inner exception message: [{unprocessableContentException.InnerException?.Message ?? string.Empty}]");

                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                context.Response.ContentType = "application/json";
                ValidationErrorResponse validationErrorResponse = CreateErrorResponse("Error was occured", unprocessableContentException);

                await context.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                ValidationErrorResponse validationErrorResponse = CreateErrorResponse("Error was occured");

                await context.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse));
            }
        }

        private ValidationErrorResponse CreateErrorResponse(string responseMessage, BaseException? excption = null)
        {
            var validationErrors = new Dictionary<string, string[]>
                {
                    { 
                        excption?.Code ?? "Wewnętrzny błąd", 
                        new[] { excption?.Message ?? "Należy zgłosić problem do admisistratora aplikacji" } 
                    }
                };

            var response = new ValidationErrorResponse
            {
                Message = responseMessage,
                Errors = validationErrors
            };

            return response;
        }
    }
}
