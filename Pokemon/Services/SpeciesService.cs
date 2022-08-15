
using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.PokeApi;

using Microsoft.EntityFrameworkCore;


namespace dotnettest.Pokemon.Services
{

    // Redis Caching docs https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-6.0#use-the-distributed-cache
    // HttpClient docs https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-6.0
    public class SpeciesService
    {
        private readonly PokemonContext _context;
        private readonly IPokeApi _pokeApi;
        private readonly ILogger _logger;
        private readonly ICache<Species> _cache;

        public SpeciesService(
            PokemonContext context,
            IPokeApi pokeApi,
            ICache<Species> cache,
            ILogger<SpeciesService> logger
        )
        {
            _context = context;
            _pokeApi = pokeApi;
            _logger = logger;
            _cache = cache;
        }

        public IEnumerable<Species> GetAll()
        {
            return _context.Species.AsNoTracking().OrderBy(p => p.Index).ToList();
        }

        public async Task<Species?> GetByIndexAsync(int index)
        {
            // try redis cache
            string cacheKey = $"species-{index}";
            Species? cachedSpecies = await _cache.Get(cacheKey);
            if (cachedSpecies is not null)
            {
                return cachedSpecies;
            }

            // try postgres
            Species? species = _context.Species.AsNoTracking().FirstOrDefault(p => p.Index == index);
            if (species is not null)
            {
                return species;
            }

            // fetch from pokeapi and save to db + cache
            _logger.LogInformation("Species not found in cache. Fetching from pokeapi", new { index });
            Species newSpecies = await _pokeApi.GetByIndexAsync(index);
            _ = _context.Species.Add(newSpecies);
            _ = await _context.SaveChangesAsync();

            await _cache.Set(cacheKey, newSpecies);

            _logger.LogInformation("Fetched from pokeapi, saved Species to database, and cached", new { index });
            return newSpecies;
        }

    }
}