using System;
using System.Threading.Tasks;

namespace FakeCompany.RateLimit;

internal interface IRateLimiter
{
    Task OnRequest(string key);

    Task<DateTime> NextAvailableRequestTime(string key);

}
