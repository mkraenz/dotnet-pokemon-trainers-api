using dotnettest.Pokemon.PokeApi;

using Microsoft.EntityFrameworkCore;

namespace dotnettest.Pokemon.Models
{
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
            Pokemon pokemon = new()
            {
                Name = apiPokemon.Name,
                Index = apiPokemon.Id,
                Link = link.ToString(),
                Type = apiPokemon.Types.First().Type.Name
            };
            return pokemon;
        }
    }
}