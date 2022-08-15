using System.ComponentModel.DataAnnotations;

using dotnettest.Pokemon.PokeApi;

namespace dotnettest.Pokemon.Models
{
    public class Species
    {
        [MaxLength(200)]
        public string Name { get; set; } = "";

        [MaxLength(50)]
        public string Type { get; set; } = "";

        /// <summary>the number in the pokedex</summary>
        [Key]
        public int Index { get; set; }

        /// <summary>example https://pokeapi.co/api/v2/pokemon/ditto</summary>
        public string Link { get; set; } = "";

        public string SpriteUrl { get; set; } = "";


        public static Species FromPokeApi(PokeApiPokemon apiPokemon, Uri link)
        {
            Species species = new()
            {
                Name = apiPokemon.Name,
                Index = apiPokemon.Id,
                Link = link.ToString(),
                Type = apiPokemon.Types.First().Type.Name,
                SpriteUrl = apiPokemon.Sprites.FrontDefault.ToString(),
            };
            return species;
        }
    }
}