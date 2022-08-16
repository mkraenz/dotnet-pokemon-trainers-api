
using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnettest.Pokemon.Controllers
{
    [ApiController]
    [Route("/api/teams")]
    public class TeamController : ControllerBase
    {
        private readonly PokemonContext _context;

        // TODO move context to service
        public TeamController(PokemonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.AsNoTracking().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Team> Get(Guid id)
        {
            Team? team = _context.Teams.Include(t => t.Members).AsNoTracking().FirstOrDefault(t => t.Id == id);
            return team is null ? NotFound() : team;
        }

        [HttpPost]
        public ActionResult<Team> Create(CreateTeamDto dto)
        {
            Team team = Team.From(dto);
            _ = _context.Teams.Add(team);
            _ = _context.SaveChanges();
            return team;
        }

        // TODO make this more RESTy
        [HttpPut("{id}/setFirst")]
        public ActionResult<Team> SetFirst(Guid id, Guid pokemonId)
        {
            // Team? team = _context.Teams.Include(t => t.Trainer).ThenInclude(tr => tr.Pokemons).AsNoTracking().FirstOrDefault(t => t.Id == id);
            // NOTE: since we want to change the entity here, we _must not_ use AsNoTracking. That would prevent any updates.
            Team? team = _context.Teams.Include(t => t.Members).FirstOrDefault(t => t.Id == id);
            Models.Pokemon? pokemon = _context.Pokemons.Include(p => p.Species).FirstOrDefault(p => p.Id == pokemonId);
            if (team is null || pokemon is null || team.TrainerId != pokemon.TrainerId)
            {
                return NotFound();
            }
            team.Members.Add(pokemon);
            _ = _context.SaveChanges();
            return team;
        }
    }
}