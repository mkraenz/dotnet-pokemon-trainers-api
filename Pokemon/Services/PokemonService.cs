using Microsoft.EntityFrameworkCore;
using TsttPokemon.Data;
using TsttPokemon.Models;

namespace TsttPokemon.Services;

public class PokemonService
{
    private readonly PokemonContext _context;
    public PokemonService(PokemonContext context)
    {
        _context = context;
    }

    public IEnumerable<Pokemon> GetAll()
    {
        return _context.Pokemons.AsNoTracking().ToList();
    }
}

