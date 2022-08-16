using System;
using System.Threading.Tasks;
using FakeCompany.RateLimit.SlidingLogCounters;
using FakeCompany.RateLimit.Store;
using Microsoft.Extensions.Configuration;

namespace FakeCompany.RateLimit.RateLimiters;

internal class SlidingWindowCounterRateLimiter : IRateLimiter
{
    private readonly int _maxRequestPerMinute;
    private readonly IAsyncDefaultedDictionary<string, SlidingLogCounter> _store;
    private readonly TimeSpan _banTime;
    private readonly TimeSpan _decayTimeInSeconds;

    public SlidingWindowCounterRateLimiter(IAsyncDefaultedDictionary<string, SlidingLogCounter> store,ConfigurationManager configuration)
    {
        var banTimeInSeconds = configuration.GetSection("RateLimit").GetValue<int>("BanTimeInSeconds");
        var decayTimeInSeconds = configuration.GetSection("RateLimit").GetValue<int>("DecayTimeInSeconds");
        
        _maxRequestPerMinute = configuration.GetSection("RateLimit").GetValue<int>("MaxRequests");
        _banTime = TimeSpan.FromSeconds(banTimeInSeconds);
        _decayTimeInSeconds = TimeSpan.FromSeconds(decayTimeInSeconds);
        
        _store = store;
    }

    public async Task OnRequest(string key)
    {
        var slider = await _store.Get(key);
        var now = DateTime.UtcNow;
        slider.EmptyTill(now);
        slider.AddRequest(now.Add(_decayTimeInSeconds));
        if (slider.Total >= _maxRequestPerMinute)
        {
            slider.Reset();
            slider.AddRequest(now.Add(_banTime),_maxRequestPerMinute);
        }
    }

    public async Task<DateTime> NextAvailableRequestTime(string key)
    {
        var slider = await _store.Get(key);
        slider.EmptyTill(DateTime.UtcNow);
        if (slider.Total >= _maxRequestPerMinute)
        {
            return slider.GetNextRequestTime();
        }
        return DateTime.MinValue;
    }
}
