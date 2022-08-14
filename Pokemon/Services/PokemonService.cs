
using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.PokeApi;

using Microsoft.EntityFrameworkCore;

namespace dotnettest.Pokemon.Services
{

    // Redis Caching docs https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-6.0#use-the-distributed-cache
    // HttpClient docs https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-6.0
    public class PokemonService
    {
        private readonly PokemonContext _context;
        private readonly IPokeApi _pokeApi;
        private readonly ILogger _logger;
        private readonly ICache<Models.Pokemon> _cache;

        public PokemonService(
            PokemonContext context,
            IPokeApi pokeApi,
            ICache<Models.Pokemon> cache,
            ILogger<PokemonService> logger
        )
        {
            _context = context;
            _pokeApi = pokeApi;
            _logger = logger;
            _cache = cache;
        }

        public IEnumerable<Models.Pokemon> GetAll()
        {
            return _context.Pokemons.AsNoTracking().OrderBy(p => p.Index).ToList();
        }

        public async Task<Models.Pokemon?> GetByIndexAsync(int index)
        {
            // try redis cache
            string cacheKey = $"pkmn{index}";
            Models.Pokemon? cachedPkmn = await _cache.Get(cacheKey);
            if (cachedPkmn is not null)
            {
                return cachedPkmn;
            }

            // try postgres
            Models.Pokemon? pokemon = _context.Pokemons.AsNoTracking().SingleOrDefault(p => p.Index == index);
            if (pokemon is not null)
            {
                return pokemon;
            }

            // fetch from pokeapi and save to db + cache
            _logger.LogInformation("Pkmn not found in cache. Fetching from pokeapi", new { index });
            Models.Pokemon newPokemon = await _pokeApi.GetByIndexAsync(index);
            _ = _context.Pokemons.Add(newPokemon);
            _ = await _context.SaveChangesAsync();

            await _cache.Set(cacheKey, newPokemon);

            _logger.LogInformation("Fetched from pokeapi, saved to database, and cached", new { index });
            return newPokemon;
        }

    }


}