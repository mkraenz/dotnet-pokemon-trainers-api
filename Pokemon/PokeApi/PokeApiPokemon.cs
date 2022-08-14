namespace TsttPokemon.PokeApi;

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class PokeApiPokemon
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = "";

    [JsonProperty("species")]
    public Species Species { get; set; }

    [JsonProperty("sprites")]
    public Sprites Sprites { get; set; }

    [JsonProperty("types")]
    public TypeElement[] Types { get; set; }

    [JsonProperty("weight")]
    public int Weight { get; set; }
}

public partial class Species
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public Uri Url { get; set; }
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
    public Species Type { get; set; }
}
