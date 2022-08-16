using System;
using System.Threading.Tasks;
using FakeCompany.RateLimit.Store;

namespace FakeCompany.RateLimit.RateLimiters;

public class TimeGapRequestRateLimiter : IRateLimiter
{
    private readonly IAsyncDefaultedDictionary<string,DateTime> _store;

    public TimeGapRequestRateLimiter(IAsyncDefaultedDictionary<string, DateTime> store)
    {
        _store = store;
    }

    public Task OnRequest(string key)
    {
        return _store.Set(key,DateTime.UtcNow);
    }

    public async Task<DateTime> NextAvailableRequestTime(string key)
    {
        var last = await _store.Get(key);
        return last.AddMinutes(5);
    }
}