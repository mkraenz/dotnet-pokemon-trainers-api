using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public ICollection<Pokemon> Pokemons { get; set; } = default!;
    }
}