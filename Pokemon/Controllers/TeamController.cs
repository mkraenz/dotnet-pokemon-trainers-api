using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Exceptions;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;

namespace dotnettest.Pokemon.Controllers
{
    [ApiController]
    [Route("/api/teams")]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teams;

        public TeamController(TeamService teams)
        {
            _teams = teams;
        }

        [HttpGet]
        public IEnumerable<Team> GetAll()
        {
            return _teams.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Team> Get(Guid id)
        {
            Team? team = _teams.Get(id);
            return team is null ? NotFound() : team;
        }

        [HttpPost]
        public ActionResult<Team> Create(CreateTeamDto dto)
        {
            Team team = Team.From(dto);
            return _teams.Create(team);
        }

        [HttpPut("{id}")]
        public ActionResult<Team> Update(Guid id, UpdateTeamDto dto)
        {
            if (!dto.HasUniqueIds())
            {
                return BadRequest("Pokemon ids must be unique. You cannot have the same pokemon in a team twice.");
            }
            try
            {
                return _teams.Update(id, dto);
            }
            catch (NotFoundException)
            {
                return NotFound("the Team or one of the Pokemons were not found.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            Team? team = _teams.Get(id);
            if (team is null)
            {
                return NotFound("The Team was not found.");
            }

            _teams.Delete(team);
            return NoContent();
        }
    }
}