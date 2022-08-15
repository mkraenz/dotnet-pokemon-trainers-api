using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IEnumerable<m.Pokemon> GetAll()
        {
            return _context.Pokemons.Include(p => p.Species).AsNoTracking().ToList();
        }


        [HttpPost]
        public async Task<ActionResult<m.Pokemon>> CreateAsync(CreatePokemonDto dto)
        {
            _logger.LogInformation($"nickname: {dto.Nickname}, speciesId: {dto.SpeciesId}, trainerId: {dto.TrainerId}, level: {dto.Level}");
            // TODO this is just a quick-and-dirty implementation for testing.
            // Move most stuff to service.
            Species? species = await _speciess.GetByIdAsync(dto.SpeciesId);
            Trainer? trainer = _trainers.Get(dto.TrainerId);

            if (species is null || trainer is null)
            {
                return NotFound("Species or Trainer not found");
            }
            m.Pokemon pokemon = m.Pokemon.FromDto(dto);
            _ = _context.Pokemons.Add(pokemon);
            _ = _context.SaveChanges();

            return pokemon;
        }
    }
}