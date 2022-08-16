using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakeCompany.RateLimit.Store;

public class DictionaryProxy<TKey,TValue> : IAsyncDefaultedDictionary<TKey,TValue> where TValue : new()
{
    private readonly Dictionary<TKey, TValue> _dictionary = new ();

    public Task<TValue> Get(TKey key)
    {
        if (!_dictionary.TryGetValue(key, out var value))
        {
            value = new TValue();
            _dictionary[key] = value;
        }
        return Task.FromResult(value);

    }

    public Task Set(TKey key, TValue value)
    {
        _dictionary[key] = value;
        return Task.CompletedTask;
    }
}
