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
            catch (UnauthorizedAccessException e)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HandleExceptionAsync(httpContext, e);
            }
            catch (BadHttpRequestException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (EntityNotFoundException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (EntryPointNotFoundException ex)
            {

                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.Error(exception: exception, message: "A new {0} was thrown: {1}", exception.GetType().Name, exception.Message);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}