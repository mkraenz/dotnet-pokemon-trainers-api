
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
            Team? team = _context.Teams.AsNoTracking().FirstOrDefault(t => t.Id == id);
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
            Team? team = _context.Teams.AsNoTracking().FirstOrDefault(t => t.Id == id);
            Models.Pokemon? pokemon = _context.Pokemons.AsNoTracking().FirstOrDefault(p => p.Id == pokemonId);
            if (team is null || pokemon is null || team.TrainerId != pokemon.TrainerId)
            {
                return NotFound();
            }
            // TODO continue here this is not working!!! How do I add entries to many to many relationships?
            team.Members.Add(pokemon);
            _ = _context.SaveChanges();
            return team;
        }
    }
}