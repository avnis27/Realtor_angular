using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface ICacheService
    {
        T Get<T>(string key);

        void Set<T>(string key, T t);

        void Remove(string key);

        void RemoveAllStartingWith(string keyPattern);
    }
}
