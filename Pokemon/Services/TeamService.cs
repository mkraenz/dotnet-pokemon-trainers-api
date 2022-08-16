using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Exceptions;
using dotnettest.Pokemon.Models;

using Microsoft.EntityFrameworkCore;

namespace dotnettest.Pokemon.Services
{
    public class TeamService
    {
        private readonly PokemonContext _context;

        public TeamService(PokemonContext context)
        {
            _context = context;

        }
        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.AsNoTracking().ToList();
        }

        public Team? Get(Guid id)
        {
            return _context.Teams
                .Include(t => t.Members)
                .ThenInclude(p => p.Species)
                .AsNoTracking()
                .FirstOrDefault(t => t.Id == id);
        }

        public Team Create(Team team)
        {
            _ = _context.Teams.Add(team);
            _ = _context.SaveChanges();
            return team;
        }

        public Team Update(Guid id, UpdateTeamDto dto)
        {
            // TODO how can we track the position f the pokemon?
            Team? team = _context.Teams.Include(t => t.Members).FirstOrDefault(t => t.Id == id);
            if (team is null)
            {
                throw new NotFoundException("The Team was not found.");
            }

            List<Models.Pokemon> pokemons = _context.Pokemons
                .Where(
                    p =>
                        dto.PokemonsAsList.Contains(p.Id)
                        && p.TrainerId == team.TrainerId
                ).ToList();
            bool allPokemonsExistAndBelongToTrainer = pokemons.Count == dto.PokemonsAsList.Count;
            if (!allPokemonsExistAndBelongToTrainer)
            {
                throw new NotFoundException("Not all pokemons exist or not all pokemons belong to the Trainer.");
            }

            team.Members = pokemons;
            team.Name = dto.Name;
            _ = _context.SaveChanges();
            return team;
        }

        internal void Delete(Team team)
        {
            _ = _context.Remove(team);
            _ = _context.SaveChanges();
        }
    }
}