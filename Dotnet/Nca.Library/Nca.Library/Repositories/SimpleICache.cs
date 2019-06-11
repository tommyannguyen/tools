using CacheManager.Core;
using System;
using System.Threading.Tasks;

namespace Nca.Library.Repositories
{
    public class SimpleICache<T> : Interfaces.ICache<T>
    {
        private ICacheManager<T> _cacheManager;
        private TimeSpan _MagicTimeOut = TimeSpan.FromMinutes(10);
        public SimpleICache()
        {
            _cacheManager = CacheFactory.Build<T>(settings =>
            {
                settings.WithSystemRuntimeCacheHandle();
            });
        }
        public T Get(string key)
        {
            return _cacheManager.Get<T>(key);
        }

        public bool Set(string key, Nca.Library.Models.CacheItem<T> value)
        {
            return _cacheManager.Add(ConvertCacheItem(key,value));
        }

        public T GetOrAdd(string key, Func<Nca.Library.Models.CacheItem<T>> valueFactory)
        {
            var havingValue = _cacheManager.TryGetOrAdd(key,
                (_) => ConvertCacheItem(key, valueFactory()),
                out CacheManager.Core.CacheItem<T> outValue);
            return havingValue ? outValue.Value : default; 
        }

        public async Task<T> GetOrAddAsync(string key, Func<Task<Nca.Library.Models.CacheItem<T>>> valueFactoryAsync)
        {
            var value = await valueFactoryAsync();
            var havingValue = _cacheManager.TryGetOrAdd(key,
                (_) => ConvertCacheItem(key, value),
                out CacheManager.Core.CacheItem<T> outValue);
            return havingValue ? outValue.Value : default(T);
        }

        /// <summary>
        /// Magic time out to make sure the timeout set outsite will be limited
        /// </summary>
        private CacheManager.Core.CacheItem<T> ConvertCacheItem(string key, Nca.Library.Models.CacheItem<T> cacheItem)
        {
            return new CacheManager.Core.CacheItem<T>(key, cacheItem.Value, ExpirationMode.Absolute, cacheItem.TimeOut > _MagicTimeOut ? cacheItem.TimeOut.Subtract(_MagicTimeOut) : cacheItem.TimeOut);
        }
    }
}
