using Microsoft.EntityFrameworkCore;
using TsttPokemon.Data;
using TsttPokemon.Models;

namespace TsttPokemon.Services;

public class TrainerService
{
    private readonly PokemonContext _context;
    public TrainerService(PokemonContext context)
    {
        _context = context;
    }


    public IEnumerable<Trainer> GetAll()
    {
        return _context.Trainers.AsNoTracking().ToList();
    }

    public Trainer? Get(Guid id)
    {
        return _context.Trainers.AsNoTracking().SingleOrDefault(p => p.Id == id);
    }

    public Trainer Create(Trainer trainer)
    {
        _context.Trainers.Add(trainer);
        _context.SaveChanges();
        return trainer;
    }

    public bool Exists(Guid id)
    {
        return Get(id) is not null;
    }
}