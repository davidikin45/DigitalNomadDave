using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Solution.Base.Implementation.Caching
{
    public enum CachePriority
    {
        Default,
        NotRemovable
    }

    public class Cache
    {
        // Gets a reference to the default MemoryCache instance. 
        private static ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy policy = null;
        private CacheEntryRemovedCallback callback = null;

        public object Add(String cacheKeyName, Object cacheItem, CachePriority cacheItemPriority, int expireMinutes)
        {
            // 
            callback = new CacheEntryRemovedCallback(this.RemovedCallback);
            policy = new CacheItemPolicy();
            policy.Priority = (cacheItemPriority == CachePriority.Default) ?
                    CacheItemPriority.Default : CacheItemPriority.NotRemovable;
            policy.SlidingExpiration = new TimeSpan(0, expireMinutes, 0);
            policy.RemovedCallback = callback;

            // Add inside cache 
            return cache.Add(cacheKeyName, cacheItem, policy);
        }

        public Object Get(String cacheKeyName)
        {
            return cache[cacheKeyName] as Object;
        }

        public T Get<T>(String cacheKeyName)
        {
            return (T)cache[cacheKeyName];
        }

        public void Remove(String cacheKeyName)
        {
            if (cache.Contains(cacheKeyName))
            {
                cache.Remove(cacheKeyName);
            }
        }

        private void RemovedCallback(CacheEntryRemovedArguments arguments)
        {
            // Log these values from arguments list 
            String strLog = String.Concat("Reason: ", arguments.RemovedReason.ToString(), " | Key - Name: ", arguments.CacheItem.Key, " | Value - Object: ", 
            arguments.CacheItem.Value.ToString());
        }
    }
}