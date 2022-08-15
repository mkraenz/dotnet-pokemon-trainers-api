using System.Text;
using System.Text.Json.Serialization;

using dotnettest.Pokemon.Models;

using Microsoft.Extensions.Caching.Distributed;

using Newtonsoft.Json;

namespace dotnettest.Pokemon.Services
{
    /// <summary>Intentionally Overengineered!
    /// This redis cache is added only for fun and learning and does not serve any useful purpose.</summary>
    public class SpeciesCacheService : ICache<Species>
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;

        public SpeciesCacheService(IDistributedCache cache, ILogger<SpeciesCacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }


        public async Task<Species?> Get(string cacheKey)
        {
            byte[]? speciesRaw = await _cache.GetAsync(cacheKey);
            if (speciesRaw is null)
            {
                return null;
            }

            _logger.LogInformation("Hit", new { cacheKey });
            string species = Encoding.UTF8.GetString(speciesRaw);
            return JsonConvert.DeserializeObject<Species>(species);
        }

        public async Task Set(string cacheKey, Species pokemon)
        {
            byte[] speciesForCache = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pokemon));
            await _cache.SetAsync(cacheKey, speciesForCache);
        }

        public async Task<bool> IsHit(string cacheKey)
        {
            var hit = await Get(cacheKey);
            return hit is not null;
        }
    }
}