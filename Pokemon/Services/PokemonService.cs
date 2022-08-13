using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using PokeApi;
using TsttPokemon.Data;
using TsttPokemon.Models;

namespace TsttPokemon.Services;

public class PokemonService
{
    private readonly PokemonContext _context;
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public PokemonService(PokemonContext context, HttpClient httpClient, ILogger<PokemonService> logger)
    {
        _context = context;
        _httpClient = httpClient;
        _logger = logger;

        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");
    }

    public IEnumerable<Pokemon> GetAll()
    {
        return _context.Pokemons.AsNoTracking().ToList();
    }

    public async Task<Pokemon?> GetByIndexAsync(int index)
    {
        var pokemon = _context.Pokemons.AsNoTracking().SingleOrDefault(p => p.Index == index);
        if (pokemon is null)
        {
            _logger.LogInformation($"Pkmn not found in cache. Fetching from pokeapi. Index: {index}");
            Uri uri = new Uri($"https://pokeapi.co/api/v2/pokemon/{index}");
            var apiPokemon = await _httpClient.GetFromJsonAsync<PokeApiPokemon>(uri);
            if (apiPokemon is null)
            {
                throw new InvalidOperationException($"Pokemon with index {index} could not be fetched from PokeApi");
            }
            Pokemon newPokemon = Pokemon.fromPokeApi(apiPokemon, uri);
            _context.Pokemons.Add(newPokemon);
            _context.SaveChanges();
            _logger.LogInformation($"Fetched from pokeapi and saved to database. Pokemon.index: {index}");
            return newPokemon;
        }
        return pokemon;
    }
}

