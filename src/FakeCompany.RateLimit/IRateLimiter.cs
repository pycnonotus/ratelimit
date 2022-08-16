using System;
using System.Threading.Tasks;

namespace FakeCompany.RateLimit.RateLimiters;

internal interface IRateLimiter
{
    Task OnRequest(string key);

    Task<DateTime> NextAvailableRequestTime(string key);

}
