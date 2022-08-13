namespace TsttPokemon.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";

    /// <summary>the number in the pokedex</summary>
    public int Order { get; set; }

    /// <summary>example https://pokeapi.co/api/v2/pokemon/ditto</summary>
    public string Link { get; set; } = "";

}