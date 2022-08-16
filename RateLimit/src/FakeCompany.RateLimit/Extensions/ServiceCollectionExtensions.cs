using System;
using FakeCompany.RateLimit.Factories;
using FakeCompany.RateLimit.Identifiers;
using FakeCompany.RateLimit.Middleware;
using FakeCompany.RateLimit.RateLimiters;
using FakeCompany.RateLimit.SlidingLogCounters;
using FakeCompany.RateLimit.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FakeCompany.RateLimit.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRateLimiting<TErrorMessageFactory>(this IServiceCollection services) where TErrorMessageFactory: class, IErrorMessageFactory  
    {
        services.AddSingleton<IIdentifier, ITokenIdentifer>();
        services.AddSingleton<IErrorMessageFactory, TErrorMessageFactory>();
        services.AddSingleton<IAsyncDefaultedDictionary<string,SlidingLogCounter>, DictionaryProxy<string,SlidingLogCounter>>();
        services.AddSingleton<IRateLimiter, SlidingWindowCounterRateLimiter>();
            
        return services;
    }
        
    public static IApplicationBuilder UseRateLimitingMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<RateLimitMiddleware>();
        return builder;
    }
}

