namespace TsttPokemon.Services;

public interface ICache<T>
{
    Task<T?> get(string key);
    Task set(string key, T obj);
}