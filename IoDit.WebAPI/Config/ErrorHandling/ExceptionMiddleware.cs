using System.Net;
using IoDit.WebAPI.Config.Exceptions;

namespace IoDit.WebAPI.Config.ErrorHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (UnauthorizedAccessException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                LogExceptionAsync(httpContext, ex);
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (BadHttpRequestException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                LogExceptionAsync(httpContext, ex);
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (EntityNotFoundException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                LogExceptionAsync(httpContext, ex);
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (EntryPointNotFoundException ex)
            {

                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                LogExceptionAsync(httpContext, ex);
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                LogExceptionAsync(httpContext, ex);
                await HandleExceptionAsync(httpContext, new Exception("Internal Server Error"));
            }
        }

        private void LogExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.Error(
                exception: exception,
                message: "A new {0} was thrown: {1} on route {2}",
                    exception.GetType().Name,
                    exception.Message,
                    context.Request.Path
            );
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new ErrorDetails
            (
                statusCode: context.Response.StatusCode,
                message: exception.Message
            ).ToString());
        }
    }
}