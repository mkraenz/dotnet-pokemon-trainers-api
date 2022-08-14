namespace dotnettest.Pokemon.PokeApi
{
    public interface IPokeApi
    {
        Task<Models.Pokemon> GetByIndexAsync(int index);
    }
}