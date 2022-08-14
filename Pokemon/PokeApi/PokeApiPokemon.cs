using Newtonsoft.Json;

namespace dotnettest.Pokemon.PokeApi
{
    public partial class PokeApiPokemon
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("species")]
        public Species Species { get; set; } = new Species();

        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; } = new Sprites();

        [JsonProperty("types")]
        public List<TypeElement> Types { get; set; } = new List<TypeElement>();

        [JsonProperty("weight")]
        public int Weight { get; set; }
    }

    public partial class Species
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("url")]
        public Uri Url { get; set; } = new Uri("");
    }

    public partial class Sprites
    {
        [JsonProperty("back_default")]
        public Uri BackDefault { get; set; } = new Uri("");

        [JsonProperty("front_default")]
        public Uri FrontDefault { get; set; } = new Uri("");
    }

    public partial class TypeElement
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public Species Type { get; set; } = new Species();
    }

}