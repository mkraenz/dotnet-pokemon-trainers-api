using Microsoft.EntityFrameworkCore;
using PokeApi;

namespace TsttPokemon.Models;

[Index(nameof(Index))]
public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";

    /// <summary>the number in the pokedex</summary>
    public int Index { get; set; }

    /// <summary>example https://pokeapi.co/api/v2/pokemon/ditto</summary>
    public string Link { get; set; } = "";

    public static Pokemon fromPokeApi(PokeApiPokemon apiPokemon, Uri link)
    {
        Pokemon pokemon = new Pokemon();
        pokemon.Name = apiPokemon.Name;
        pokemon.Index = apiPokemon.Id;
        pokemon.Link = link.ToString();
        pokemon.Type = apiPokemon.Types.First().Type.Name;
        return pokemon;
    }
}