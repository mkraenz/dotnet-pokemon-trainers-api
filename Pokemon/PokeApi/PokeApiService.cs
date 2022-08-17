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

        public async Task<Models.Species> GetByIdAsync(int id)
        {
            Uri uri = new($"https://pokeapi.co/api/v2/pokemon/{id}");
            PokeApiPokemon? apiPokemon = await _httpClient.GetFromJsonAsync<PokeApiPokemon>(uri);
            return apiPokemon is null
                ? throw new InvalidOperationException($"The Pokemon with id {id} could not be fetched from PokeApi.")
                : PokeApiPokemon.ToSpecies(apiPokemon, uri);
        }

    }
}