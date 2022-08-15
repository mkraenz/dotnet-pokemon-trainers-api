using System.ComponentModel.DataAnnotations;

using dotnettest.Pokemon.PokeApi;

namespace dotnettest.Pokemon.Models
{
    public class Species
    {
        /// <summary>the number in the pokedex</summary>
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = "";

        [MaxLength(50)]
        public string Type { get; set; } = "";


        /// <summary>example https://pokeapi.co/api/v2/pokemon/ditto</summary>
        public string Link { get; set; } = "";

        public string SpriteUrl { get; set; } = "";

        public ICollection<Pokemon>? Pokemons { get; set; }


        public static Species FromPokeApi(PokeApiPokemon apiPokemon, Uri link)
        {
            Species species = new()
            {
                Name = apiPokemon.Name,
                Id = apiPokemon.Id,
                Link = link.ToString(),
                Type = apiPokemon.Types.First().Type.Name,
                SpriteUrl = apiPokemon.Sprites.FrontDefault.ToString(),
            };
            return species;
        }

    }
}