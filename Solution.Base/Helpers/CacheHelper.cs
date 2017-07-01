using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Solution.Base.Helpers
{
    public static class CacheHelper
    {
        public static void ClearOutputCache()
        {
            var runtimeType = typeof(HttpRuntime);

            var ci = runtimeType.GetProperty(
               "CacheInternal",
               BindingFlags.NonPublic | BindingFlags.Static);

            var cache = ci.GetValue(ci, new object[0]);

            var cachesInfo = cache.GetType().GetField(
                "_caches",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var cacheEntries = cachesInfo.GetValue(cache);

            var outputCacheEntries = new List<object>();

            foreach (Object singleCache in cacheEntries as Array)
            {
                var singleCacheInfo =
                singleCache.GetType().GetField("_entries",
                   BindingFlags.NonPublic | BindingFlags.Instance);
                var entries = singleCacheInfo.GetValue(singleCache);

                foreach (DictionaryEntry cacheEntry in entries as Hashtable)
                {
                    var cacheEntryInfo = cacheEntry.Value.GetType().GetField("_value",
                       BindingFlags.NonPublic | BindingFlags.Instance);
                    var value = cacheEntryInfo.GetValue(cacheEntry.Value);
                    if (value.GetType().Name == "CachedRawResponse")
                    {
                        var key = (string)cacheEntry.Value.GetType().BaseType.GetField("_key", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(cacheEntry.Value);
                        key = key.Substring(key.IndexOf("/"));
                        outputCacheEntries.Add(key);
                    }
                }
            }
            foreach (string key in outputCacheEntries)
            {
                HttpResponse.RemoveOutputCacheItem(key);
            }
        }
    }
}
