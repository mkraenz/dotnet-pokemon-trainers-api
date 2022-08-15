using dotnettest.Pokemon.Data;
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
#nullable disable warnings
            return _context.Trainers.Include(t => t.Pokemons).ThenInclude(p => p.Species).AsNoTracking().ToList();
#nullable enable warnings
        }

        public Trainer? Get(Guid id)
        {
            // Note: == works because GUID overloads == operator to use .Equals
            return _context.Trainers.AsNoTracking().FirstOrDefault(p => p.Id == id);
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
    }
}