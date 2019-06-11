using Nca.Library.Models;
using System;
using System.Threading.Tasks;

namespace Nca.Library.Interfaces
{
    public interface ICache<T>
    {
        T Get(string key);
        bool Set(string key, CacheItem<T> value);
        Task<T> GetOrAddAsync(string key, Func<Task<CacheItem<T>>> valueFactoryAsync);
        T GetOrAdd(string key, Func<CacheItem<T>> valueFactory);
    }
}
