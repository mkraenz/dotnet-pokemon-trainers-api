namespace dotnettest.Pokemon.Services
{
    public interface ICache<T>
    {
        Task<T?> Get(string key);
        Task Set(string key, T obj);
    }
}