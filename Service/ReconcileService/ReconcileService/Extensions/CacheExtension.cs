using System;
using Microsoft.Extensions.Caching.Memory;

namespace ReconcileService.Extensions
{
    /// <summary>
    /// This static class provides extension methods for IMemoryCache to implement a cache-aside pattern.
    /// </summary>
    public static class CacheExtension
	{
        /// <summary>
        /// Asynchronously retrieves a value from the cache with the specified key.
        /// If the value is not found in the cache, it calls the provided asynchronous function to fetch the data, stores it in the cache with a sliding expiration, and then returns it.
        /// </summary>
        /// <typeparam name="T">The type of the cached data.</typeparam>
        /// <param name="_cache">The IMemoryCache instance to interact with.</param>
        /// <param name="key">The unique key for the cached data.</param>
        /// <param name="func">An asynchronous function that retrieves the data if it's not found in the cache.</param>
        /// <returns>An asynchronous task that returns the value from the cache or the result of the provided function.</returns>
        public async static Task<T> GetOrSetAsync<T>(this IMemoryCache _cache, string key, Func<Task<T>> func)
        {
            T t;
            if (!_cache.TryGetValue(key, out t))
            {
                t = await func();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(key, t, cacheEntryOptions);
            }

            return t;
        }

        /// <summary>
        /// Retrieves a value from the cache with the specified key.
        /// If the value is not found in the cache, it calls the provided function to get the data, stores it in the cache with a sliding expiration, and then returns it.
        /// </summary>
        /// <typeparam name="T">The type of the cached data.</typeparam>
        /// <param name="_cache">The IMemoryCache instance to interact with (passed by reference).</param>
        /// <param name="key">The unique key for the cached data.</param>
        /// <param name="func">A function that retrieves the data if it's not found in the cache.</param>
        /// <returns>The value from the cache or the result of the provided function.</returns>
        public static T GetOrSet<T>(ref IMemoryCache _cache, string key, Func<T> func)
        {
            T t;
            if (!_cache.TryGetValue(key, out t))
            {
                t = func();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(key, t, cacheEntryOptions);
            }

            return t;
        }
    }
}

