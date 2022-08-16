using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FakeCompany.RateLimit.Extensions;
using FakeCompany.RateLimit.Factories;
using FakeCompany.RateLimit.Identifiers;
using FakeCompany.RateLimit.RateLimiters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;

namespace FakeCompany.RateLimit.Middleware;
internal class RateLimitMiddleware
{

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    
    
    private readonly RequestDelegate _next;
    private readonly IIdentifier _identifier;
    private readonly IRateLimiter _rateLimiter;
    private readonly IErrorMessageFactory _errorMessageFactory;
    
    public RateLimitMiddleware(
        RequestDelegate next, 
        IIdentifier identifier,
        IRateLimiter rateLimiter,
        IErrorMessageFactory errorMessageFactory
    )
    {
        _next=next;
        _identifier = identifier;
        _rateLimiter = rateLimiter;
        _errorMessageFactory = errorMessageFactory;
    }
        
    public async Task InvokeAsync(HttpContext context)        
    {
        var descriptor = context.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();
        var decorator = descriptor?.GetRateLimitedAttribute();

        if (decorator is null)
        {
            await _next(context);   
            return;
        }        
            
        var key = _identifier.Identify(context);
        if (key is null)
        {
            await OnUserUnauthenticated(context);
            return;
        }
        var nextAvailableRequestTime = await _rateLimiter.NextAvailableRequestTime(key);
        
        if (nextAvailableRequestTime >= DateTime.UtcNow)
        {
            await OnUserHitLimit(context, nextAvailableRequestTime);
            return;
        }
        
        await _rateLimiter.OnRequest(key);  
        await _next(context);       
    }

    private async Task OnUserHitLimit(HttpContext context, DateTime until)
    {

        var error = _errorMessageFactory.CrateError(until);
        var json = JsonSerializer.Serialize(error,_jsonSerializerOptions);
        await context.Response.WriteAsync(json);
    }

    private  Task OnUserUnauthenticated(HttpContext context)
    {

        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return Task.CompletedTask;

    }
}
