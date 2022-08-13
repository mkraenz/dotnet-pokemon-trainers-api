using Microsoft.EntityFrameworkCore;
using TsttPokemon.Data;
using TsttPokemon.Models;
using TsttPokemon.PokeApi;

namespace TsttPokemon.Services;


// Redis Caching docs https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-6.0#use-the-distributed-cache
// HttpClient docs https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-6.0
public class PokemonService
{
    private readonly PokemonContext _context;
    private readonly IPokeApi _pokeApi;
    private readonly ILogger _logger;
    private readonly ICache<Pokemon> _cache;

    public PokemonService(
        PokemonContext context,
        IPokeApi pokeApi,
        ICache<Pokemon> cache,
        ILogger<PokemonService> logger
    )
    {
        _context = context;
        _pokeApi = pokeApi;
        _logger = logger;
        _cache = cache;
    }

    public IEnumerable<Pokemon> GetAll()
    {
        return _context.Pokemons.AsNoTracking().OrderBy(p => p.Index).ToList();
    }

    public async Task<Pokemon?> GetByIndexAsync(int index)
    {
        // try redis cache
        String cacheKey = $"pkmn{index}";
        Pokemon? cachedPkmn = await _cache.get(cacheKey);
        if (cachedPkmn is not null)
            return cachedPkmn;

        // try postgres
        var pokemon = _context.Pokemons.AsNoTracking().SingleOrDefault(p => p.Index == index);
        if (pokemon is not null)
            return pokemon;

        // fetch from pokeapi and save to db + cache
        _logger.LogInformation($"Pkmn not found in cache. Fetching from pokeapi. Index: {index}");
        Pokemon newPokemon = await _pokeApi.getByIndexAsync(index);
        _context.Pokemons.Add(newPokemon);
        await _context.SaveChangesAsync();

        await _cache.set(cacheKey, newPokemon);

        _logger.LogInformation($"Fetched from pokeapi, saved to database, and cached. Pokemon.index: {index}");
        return newPokemon;
    }

}

