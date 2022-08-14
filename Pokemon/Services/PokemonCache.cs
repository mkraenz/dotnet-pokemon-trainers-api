using System.Text;
using System.Text.Json.Serialization;

using dotnettest.Pokemon.Models;

using Microsoft.Extensions.Caching.Distributed;

using Newtonsoft.Json;

namespace dotnettest.Pokemon.Services
{
    /// <summary>Intentionally Overengineered!</summary>
    public class PokemonCache : ICache<Models.Pokemon>
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;

        public PokemonCache(IDistributedCache cache, ILogger<PokemonCache> logger)
        {
            _cache = cache;
            _logger = logger;
        }


        public async Task<Models.Pokemon?> Get(string cacheKey)
        {
            byte[]? cachedPkmnRaw = await _cache.GetAsync(cacheKey);
            if (cachedPkmnRaw is null)
            {
                return default;
            }

            _logger.LogInformation("Hit", new { cacheKey });
            string cachedPkmn = Encoding.UTF8.GetString(cachedPkmnRaw);
            return JsonConvert.DeserializeObject<Models.Pokemon>(cachedPkmn);
        }

        public async Task Set(string cacheKey, Models.Pokemon pokemon)
        {
            byte[] pokemonForCache = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pokemon));
            await _cache.SetAsync(cacheKey, pokemonForCache);
        }

        public async Task<bool> IsHit(string cacheKey)
        {
            var hit = await Get(cacheKey);
            return hit is not null;
        }
    }
}