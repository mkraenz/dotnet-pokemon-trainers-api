using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PokeApi;
using TsttPokemon.Data;
using TsttPokemon.Models;

namespace TsttPokemon.Services;

public class PokemonService
{
    private readonly PokemonContext _context;
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly IDistributedCache _cache;

    public PokemonService(PokemonContext context, HttpClient httpClient, IDistributedCache cache, ILogger<PokemonService> logger)
    {
        _context = context;
        _httpClient = httpClient;
        _logger = logger;
        _cache = cache;

        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");
    }

    public IEnumerable<Pokemon> GetAll()
    {
        return _context.Pokemons.AsNoTracking().ToList();
    }

    public async Task<Pokemon?> GetByIndexAsync(int index)
    {
        // try redis cache
        String cacheKey = $"pkmn{index}";
        Pokemon? cachedPkmn = await getPokemonFromCache(cacheKey);
        if (cachedPkmn is not null)
            return cachedPkmn;

        // try postgres
        var pokemon = _context.Pokemons.AsNoTracking().SingleOrDefault(p => p.Index == index);
        if (pokemon is not null)
            return pokemon;

        // fetch from pokeapi and save to db + cache
        _logger.LogInformation($"Pkmn not found in cache. Fetching from pokeapi. Index: {index}");
        Uri uri = new Uri($"https://pokeapi.co/api/v2/pokemon/{index}");
        PokeApiPokemon? apiPokemon = await _httpClient.GetFromJsonAsync<PokeApiPokemon>(uri);
        if (apiPokemon is null)
        {
            throw new InvalidOperationException($"Pokemon with index {index} could not be fetched from PokeApi");
        }
        Pokemon newPokemon = Pokemon.fromPokeApi(apiPokemon, uri);
        _context.Pokemons.Add(newPokemon);
        await _context.SaveChangesAsync();

        await setPokemonInCache(cacheKey, newPokemon);

        _logger.LogInformation($"Fetched from pokeapi, saved to database, and cached. Pokemon.index: {index}");
        return newPokemon;
    }

    private async Task<Pokemon?> getPokemonFromCache(string cacheKey)
    {
        var cachedPkmnRaw = await _cache.GetAsync(cacheKey);
        if (cachedPkmnRaw is null)
            return null;
        Console.WriteLine($"Cache hit for {cacheKey}");
        var cachedPkmn = Encoding.UTF8.GetString(cachedPkmnRaw);
        return JsonConvert.DeserializeObject<Pokemon>(cachedPkmn);
    }

    private async Task setPokemonInCache(string cacheKey, Pokemon pokemon)
    {
        byte[] pokemonForCache = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pokemon));
        await _cache.SetAsync(cacheKey, pokemonForCache);
    }
}

