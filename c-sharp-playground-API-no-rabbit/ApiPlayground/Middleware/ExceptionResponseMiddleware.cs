
using ApiPlayground.controllers;
using Newtonsoft.Json;

namespace ApiPlayground.Middleware;

public class ExceptionResponseMiddleware
{
    private RequestDelegate _next;
    private readonly ILogger<ExceptionResponseMiddleware> _logger;

    public ExceptionResponseMiddleware(RequestDelegate next, ILogger<ExceptionResponseMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin","http://localhost:3000");
            await _next(context);
        }
        catch (ArgumentException)
        {
            
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse()
                {ErrorMessage = "Invalid Calculation"}));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse()
                {ErrorMessage = "Unexpected error"}));
        }
        
    }
}