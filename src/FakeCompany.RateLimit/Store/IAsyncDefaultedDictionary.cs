using System.Threading.Tasks;

namespace FakeCompany.RateLimit.Store;

/// <summary>
///  Defaulted Dictionary is like a regular <a href="https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2?view=net-6.0" >Dictionary </a> ,<br />
///  but when trying to get the value of uninitialized key the Dictionary will return's  default of TValue and initialize the key with that value.
/// </summary>
/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
public interface IAsyncDefaultedDictionary<TKey,TValue>
{
    Task<TValue> Get(TKey key);

    Task Set(TKey key, TValue value);
}
