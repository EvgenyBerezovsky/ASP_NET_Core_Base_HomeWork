using Microsoft.Extensions.Logging;
using Lesson007.Exeptions;

namespace Lesson007.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (LogServiceDebugException ex)
            {
                _logger.LogDebug("Поймано исключение LogServiceDebugExeption: {Message}", ex.Message);

                context.Response.StatusCode = 429;
                context.Response.ContentType = "application/json";

                await  context.Response.WriteAsync("false");
            }

            catch (LogServiceErrorException ex)
            {
                _logger.LogError("Поймано исключение LogServiceErrorExeption: {Message}", ex.Message);

                context.Response.StatusCode = 429;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("false");
            }

            catch (LogServiceInformationException ex)
            {
                _logger.LogInformation("Поймано исключение LogServiceInformationExeption: {Message}", ex.Message);

                context.Response.StatusCode = 429;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("false");
            }

            catch (LogServiceWarningException ex)
            {
                _logger.LogWarning("Поймано исключение LogServiceWarningExeption: {Message}", ex.Message);

                context.Response.StatusCode = 429;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("false");
            }
        }
    }
}
