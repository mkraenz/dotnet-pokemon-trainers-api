using dotnettest.Pokemon.Models;

using Microsoft.EntityFrameworkCore;

namespace dotnettest.Pokemon.Data
{
    public class PokemonContext : DbContext
    {
        public DbSet<Models.Pokemon> Pokemons => Set<Models.Pokemon>();
        public DbSet<Trainer> Trainers => Set<Trainer>();

        protected readonly IConfiguration Configuration;

        public PokemonContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
    }
}