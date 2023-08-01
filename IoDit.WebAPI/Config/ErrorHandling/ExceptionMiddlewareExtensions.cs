using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using IoDit.WebAPI.Config.ErrorHandling;

namespace IoDit.WebAPI.ErrorHandling;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}