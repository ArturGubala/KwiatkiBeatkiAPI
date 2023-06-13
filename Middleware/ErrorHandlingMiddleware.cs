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
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                ValidationErrorResponse validationErrorResponse = CreateErrorResponse("Error was occured", badRequestException);

                if (badRequestException.InnerException != null)
                {
                    _logger.LogError(badRequestException.InnerException.Message);
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse));
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                ValidationErrorResponse validationErrorResponse = CreateErrorResponse("Error was occured", notFoundException);

                if (notFoundException.InnerException != null)
                {
                    _logger.LogError(notFoundException.InnerException.Message);
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse));
            }
            catch (UnprocessableContentException unprocessableContentException)
            {
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                context.Response.ContentType = "application/json";
                ValidationErrorResponse validationErrorResponse = CreateErrorResponse("Error was occured", unprocessableContentException);

                if (unprocessableContentException.InnerException != null)
                {
                    _logger.LogError(unprocessableContentException.InnerException.Message);
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Something went wrong");
            }
        }

        private ValidationErrorResponse CreateErrorResponse(string responseMessage, BaseException exeption)
        {
            var validationErrors = new Dictionary<string, string[]>
                {
                    { exeption.Code, new[] { exeption.Message } }
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
