using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TsttPokemon.Models;

namespace TsttPokemon.Services;

public class PokemonCache : ICache<Pokemon>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger _logger;

    public PokemonCache(IDistributedCache cache, ILogger<PokemonCache> logger)
    {
        _cache = cache;
        _logger = logger;
    }


    public async Task<Pokemon?> get(string cacheKey)
    {
        var cachedPkmnRaw = await _cache.GetAsync(cacheKey);
        if (cachedPkmnRaw is null)
            return null;
        _logger.LogInformation($"Hit for key: {cacheKey}");
        var cachedPkmn = Encoding.UTF8.GetString(cachedPkmnRaw);
        return JsonConvert.DeserializeObject<Pokemon>(cachedPkmn);
    }

    public async Task set(string cacheKey, Pokemon pokemon)
    {
        byte[] pokemonForCache = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pokemon));
        await _cache.SetAsync(cacheKey, pokemonForCache);
    }
}