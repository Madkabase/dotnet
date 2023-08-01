using System.Net;

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
                _logger.Error(exception: e, message: "A new Unauthorized exception has been thrown: {0}", e.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HandleExceptionAsync(httpContext, e);
            }
            catch (BadHttpRequestException ex)
            {
                _logger.Error(exception: ex, message: "A new BadHttpRequestException exception has been thrown: {0}", ex.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (EntryPointNotFoundException ex)
            {
                _logger.Error("A new EntryPointNotFoundException exception has been thrown: {0}", ex.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.Error(exception: ex, message: "Something went wrong: {0}", ex.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}