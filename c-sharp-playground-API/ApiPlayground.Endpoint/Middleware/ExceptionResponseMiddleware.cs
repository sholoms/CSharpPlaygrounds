
using ApiPlayground.controllers;
using Newtonsoft.Json;

namespace ApiPlayground.Middleware;

public class ExceptionResponseMiddleware
{
    private RequestDelegate _next;

    public ExceptionResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse()
                {ErrorMessage = "Invalid Calculation"}));
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse()
                {ErrorMessage = "Unexpected error"}));
        }
        
    }
}