using System;

namespace FakeCompany.RateLimit.RateLimiters;

public class TimeStampAndCounter
{
    private readonly DateTime _decayTime;

    public TimeStampAndCounter(DateTime time)
    {
        DecayTime = time;
    }
    public TimeStampAndCounter(DateTime time, int counter)
    {
        DecayTime = time;
        Counter = counter;
    }

    public DateTime DecayTime
    {
        get => _decayTime;
        private init => _decayTime = new DateTime(value.Year,value.Month,value.Day,value.Hour,value.Minute,value.Second);
    }

    public int Counter { get; private set; } = 1;

    public void IncrementCounter(int amount)
    {
        Counter+=amount;
    }

    public bool CanZip(DateTime time)
    {
        var different = this.DecayTime - time;
        return different <= TimeSpan.FromSeconds(1);
    }
    
}
