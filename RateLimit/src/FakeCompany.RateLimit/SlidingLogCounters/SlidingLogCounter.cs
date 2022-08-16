using System;
using System.Collections.Generic;

namespace FakeCompany.RateLimit.SlidingLogCounters;

public class SlidingLogCounter
{
    private int _total = 0;
    
    private readonly Queue<TimeStampAndCounter> _queue = new ();
    private TimeStampAndCounter _lastElement = null;
    
    
    public SlidingLogCounter() { }

    public void EmptyTill(DateTime time)
    {
        while (_queue.Count == 0 &&_queue.Peek()?.DecayTime < time)
        {
            var stamp = _queue.Dequeue();
            _total -= stamp.Counter;
        }
    }


    public void AddRequest(DateTime decayTime, int amount = 1)
    {
        _total+=amount;
        
        if (_lastElement?.CanZip(decayTime) ?? false)
        {
            _lastElement.IncrementCounter(amount);
            return;
        }
        
        _lastElement = new TimeStampAndCounter(decayTime);
        _queue.Enqueue(_lastElement);
        
       
    }

    public void Reset()
    {
        _total = 0;
        _lastElement = null;
        _queue.Clear();

    }


    public DateTime GetNextRequestTime()
    {
        return _queue.Count == 0 ? DateTime.MinValue : _queue.Peek().DecayTime;
    }
   
    
    public int Total => _total;
}
