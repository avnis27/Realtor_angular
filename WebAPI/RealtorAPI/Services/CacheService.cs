using Microsoft.Extensions.Caching.Memory;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Services
{
    public class CacheService : ICacheService
    {
        private List<string> Keys { get; set; }
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
            Keys = new List<string>();
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Set<T>(string key, T t)
        {
            if (!Keys.Exists(k => k == key))
                Keys.Add(key);
            _memoryCache.Set(key, t);
        }

        public void Remove(string key)
        {
            if (Keys.Exists(k => k == key))
            {
                Keys.Remove(key);
                _memoryCache.Remove(key);
            }
        }

        public void RemoveAllStartingWith(string keyPattern)
        {
            var keys = Keys.FindAll(k => k.StartsWith(keyPattern));
            foreach (var key in keys)
            {
                Remove(key);
            }
        }
    }
}
