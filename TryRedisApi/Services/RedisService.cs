using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace TryRedisApi.Services
{
    public class RedisService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }

        public T? GetCachedData<T>(string key)
        {
            var jsonData = _distributedCache.GetString(key);
            if (jsonData == null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public void SaveCachedData<T>(string key, T data, TimeSpan cacheDuration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuration,
            };
            var jsonData = JsonSerializer.Serialize(data);
            _distributedCache.SetString(key, jsonData, options);
        }
    }
}
