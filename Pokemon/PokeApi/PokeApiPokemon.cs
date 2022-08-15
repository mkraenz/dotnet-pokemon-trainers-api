using System.Text.Json.Serialization;

namespace dotnettest.Pokemon.PokeApi
{
    public partial class PokeApiPokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("species")]
        public Species Species { get; set; } = new Species();

        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; } = new Sprites();

        [JsonPropertyName("types")]
        public List<TypeElement> Types { get; set; } = new List<TypeElement>();

        [JsonPropertyName("weight")]
        public int Weight { get; set; }
    }

    public partial class Species
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("url")]
        public string Url { get; set; } = "";
    }

    public partial class Sprites
    {
        [JsonPropertyName("back_default")]
        public string BackDefault { get; set; } = "";

        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; } = "";
    }

    public partial class TypeElement
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public Species Type { get; set; } = new Species();
    }

}