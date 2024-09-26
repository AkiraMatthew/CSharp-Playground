using Microsoft.AspNetCore.Http;

public class Middleware
{
    private readonly RequestDelegate _next;

    public Middleware(RequestDelegate next)
    {
        next = _next;
    }

    public async Task InvokeAsync(HttpContext context) { }
}