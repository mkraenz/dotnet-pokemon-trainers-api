namespace TsttPokemon.PokeApi;

using Microsoft.Net.Http.Headers;
using TsttPokemon.Models;

public class PokeApiService : IPokeApi
{
    private readonly HttpClient _httpClient;
    public PokeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");
    }

    public async Task<Pokemon> getByIndexAsync(int index)
    {
        Uri uri = new Uri($"https://pokeapi.co/api/v2/pokemon/{index}");
        PokeApiPokemon? apiPokemon = await _httpClient.GetFromJsonAsync<PokeApiPokemon>(uri);
        if (apiPokemon is null)
        {
            throw new InvalidOperationException($"Pokemon with index {index} could not be fetched from PokeApi");
        }
        return Pokemon.fromPokeApi(apiPokemon, uri);
    }

}