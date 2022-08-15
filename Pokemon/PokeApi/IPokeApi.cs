namespace dotnettest.Pokemon.PokeApi
{
    public interface IPokeApi
    {
        Task<Models.Species> GetByIndexAsync(int index);
    }
}