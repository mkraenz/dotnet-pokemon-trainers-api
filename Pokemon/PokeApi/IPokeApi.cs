namespace dotnettest.Pokemon.PokeApi
{
    public interface IPokeApi
    {
        Task<Models.Species> GetByIdAsync(int id);
    }
}