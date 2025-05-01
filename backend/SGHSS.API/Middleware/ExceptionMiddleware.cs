using Infrastructure.CrossCutting.Utils;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Api.Middleware;

[ExcludeFromCodeCoverage]
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
    }

    public async Task Invoke(HttpContext httpContext)
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

    private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        httpContext.Request.EnableBuffering();

        if (ex is FluentValidationException fluentEx)
        {
            _logger.LogWarning($"Validation error: {fluentEx.Message}");
            return ReturnFluentValidationException(httpContext, fluentEx);
        }

        if (ex is ApiException apiEx)
        {
            _logger.LogError($"API error: {apiEx.Message}");
            return ReturnApiException(httpContext, apiEx);
        }

        _logger.LogError($"Unexpected error: {ex.Message}");
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorStandardResponse 
        { 
            Message = "Ocorreu um erro inesperado.", 
            StatusCode = httpContext.Response.StatusCode 
        }));
    }

    private static Task ReturnFluentValidationException(HttpContext httpContext, FluentValidationException ex)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        httpContext.Response.ContentType = "application/json";

        return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorStandardResponse 
        { 
            Message = ex.Message, 
            StatusCode = httpContext.Response.StatusCode,
            IsSuccessful = false
        }));
    }

    private static Task ReturnApiException(HttpContext httpContext, ApiException ex)
    {
        httpContext.Response.StatusCode = (int)ex.StatusCode;
        httpContext.Response.ContentType = "application/json";

        return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorStandardResponse 
        { 
            Message = ex.Message, 
            StatusCode = httpContext.Response.StatusCode,
            IsSuccessful = false
        }));
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionMiddleware>();
}

[Newtonsoft.Json.JsonObject(NamingStrategyType = typeof(Newtonsoft.Json.Serialization.CamelCaseNamingStrategy))]
internal class ErrorStandardResponse
{
    public string? Message { get; set; }
    public bool IsSuccessful { get; set; } = false;
    public object? Data { get; set; } = null;
    public int StatusCode { get; set; } = (int)HttpStatusCode.InternalServerError;
}