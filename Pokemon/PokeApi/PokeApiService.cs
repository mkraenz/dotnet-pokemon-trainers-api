using Microsoft.Net.Http.Headers;

namespace dotnettest.Pokemon.PokeApi
{
    public class PokeApiService : IPokeApi
    {
        private readonly HttpClient _httpClient;
        public PokeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/json");
        }

        public async Task<Models.Pokemon> GetByIndexAsync(int index)
        {
            Uri uri = new($"https://pokeapi.co/api/v2/pokemon/{index}");
            PokeApiPokemon? apiPokemon = await _httpClient.GetFromJsonAsync<PokeApiPokemon>(uri);
            return apiPokemon is null
                ? throw new InvalidOperationException($"Pokemon with index {index} could not be fetched from PokeApi")
                : Models.Pokemon.fromPokeApi(apiPokemon, uri);
        }

    }
}