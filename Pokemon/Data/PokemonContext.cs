using Microsoft.EntityFrameworkCore;
using TsttPokemon.Models;

namespace TsttPokemon.Data;

public class PokemonContext : DbContext
{
    public DbSet<Pokemon> Pokemons => Set<Pokemon>();

    protected readonly IConfiguration Configuration;

    public PokemonContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));

}