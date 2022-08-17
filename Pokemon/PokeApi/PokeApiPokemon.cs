using System.Text.Json.Serialization;

using dotnettest.Pokemon.Models;

namespace dotnettest.Pokemon.PokeApi
{
    public partial class PokeApiPokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("species")]
        public PokeApiSpecies Species { get; set; } = new PokeApiSpecies();

        [JsonPropertyName("sprites")]
        public PokeApiSprites Sprites { get; set; } = new PokeApiSprites();

        [JsonPropertyName("types")]
        public List<PokeApiTypeElement> Types { get; set; } = new List<PokeApiTypeElement>();

        [JsonPropertyName("weight")]
        public int Weight { get; set; }


        public static Species ToSpecies(PokeApiPokemon apiPokemon, Uri link)
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

    public partial class PokeApiSpecies
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("url")]
        public string Url { get; set; } = "";
    }

    public partial class PokeApiSprites
    {
        [JsonPropertyName("back_default")]
        public string BackDefault { get; set; } = "";

        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; } = "";
    }

    public partial class PokeApiTypeElement
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public PokeApiSpecies Type { get; set; } = new PokeApiSpecies();
    }

}