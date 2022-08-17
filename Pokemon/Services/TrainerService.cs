using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Exceptions;
using dotnettest.Pokemon.Models;

using Microsoft.EntityFrameworkCore;

namespace dotnettest.Pokemon.Services
{
    public class TrainerService
    {
        private readonly PokemonContext _context;
        public TrainerService(PokemonContext context)
        {
            _context = context;
        }


        public IEnumerable<Trainer> GetAll()
        {
            // https://github.com/dotnet/efcore/issues/17212#issuecomment-522188174
            // in the sql the null is handled properly, thus disabling warning 
            return _context.Trainers.Include(t => t.Pokemons).ThenInclude(p => p.Species).AsNoTracking().ToList();
        }

        public IEnumerable<Trainer> GetAllWithTeams()
        {
            // https://github.com/dotnet/efcore/issues/17212#issuecomment-522188174
            // in the sql the null is handled properly, thus disabling warning 
            return _context.Trainers.Include(t => t.Teams).ThenInclude(p => p.Members).ThenInclude(p => p.Species).AsNoTracking().ToList();
        }

        public Trainer? Get(Guid id)
        {
            // Note: == works because GUID overloads == operator to use .Equals
            return _context.Trainers.Include(t => t.Pokemons).ThenInclude(p => p.Species).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public Trainer Create(Trainer trainer)
        {
            _ = _context.Trainers.Add(trainer);
            _ = _context.SaveChanges();
            return trainer;
        }

        public bool Exists(Guid id)
        {
            return Get(id) is not null;
        }

        public bool EmailExists(string email)
        {
            return _context.Trainers.AsNoTracking().Any(p => p.Email.ToLower() == email.ToLower());
        }

        public void Update(Guid id, UpdateTrainerDto dto)
        {
            Trainer? trainer = _context.Trainers.Include(t => t.Teams).FirstOrDefault(t => t.Id == id);
            if (trainer is null)
            {
                throw new NotFoundException();
            }

            _ = _context.Update(trainer);

            if (dto.Name is not null)
            {
                trainer.Name = dto.Name;
            }

            if (dto.ActiveTeamId is not null)
            {
                bool teamExistsOnTrainer = trainer.Teams.Any(t => t.TrainerId == trainer.Id);
                trainer.ActiveTeamId = teamExistsOnTrainer
                    ? dto.ActiveTeamId
                    : throw new NotFoundException("The Team with given ActiveTeamId was not found on the Trainer.");
            }
            _ = _context.SaveChanges();
        }
    }
}