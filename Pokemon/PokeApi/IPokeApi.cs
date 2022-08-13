

using TsttPokemon.Models;

namespace TsttPokemon.PokeApi;

public interface IPokeApi
{
    Task<Pokemon> getByIndexAsync(int index);
}
