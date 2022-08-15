using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;

using m = dotnettest.Pokemon.Models;

namespace dotnettest.Pokemon.Controllers
{
    [ApiController]
    [Route("api/pokemons")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonContext _context;
        private readonly SpeciesService _speciess;
        private readonly TrainerService _trainers;
        private readonly ILogger _logger;

        public PokemonController(SpeciesService speciess, TrainerService trainers, PokemonContext context, ILogger<PokemonController> logger)
        {
            _context = context;
            _speciess = speciess;
            _trainers = trainers;
            _logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<m.Pokemon>> CreateAsync(int speciesId, Guid trainerId, int level, string? nickname)
        {
            _logger.LogInformation($"nickname: {nickname}, speciesId: {speciesId}, trainerId: {trainerId}, level: {level}");
            // TODO this is just a quick-and-dirty implementation for testing.
            // Move most stuff to service.
            Species? species = await _speciess.GetByIdAsync(speciesId);
            Trainer? trainer = _trainers.Get(trainerId);

            if (species is null || trainer is null)
            {
                return NotFound("Species or Trainer not found");
            }
            m.Pokemon pokemon = new()
            {
                Level = level,
                SpeciesId = speciesId,
                TrainerId = trainerId,
                Nickname = nickname
            };
            _ = _context.Pokemons.Add(pokemon);
            _ = _context.SaveChanges();

            return pokemon;
        }
    }
}