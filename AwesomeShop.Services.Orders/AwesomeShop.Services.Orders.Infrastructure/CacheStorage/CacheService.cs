using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Infrastructure.CacheStorage
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _cache.GetStringAsync(key);

            if(string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine($"Cache key {key} not found");
                return default;
            }

            Console.WriteLine($"Cache key {key} found");

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };

            var serializedValue = JsonConvert.SerializeObject(value);

            Console.WriteLine($"Setting cache key: {key}");

            await _cache.SetStringAsync(key, serializedValue, options);
        }
    }
}
