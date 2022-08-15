using dotnettest.Pokemon.Models;

using Microsoft.EntityFrameworkCore;

// to avoid conflict of Pokemon model and Pokemon namespace  
using m = dotnettest.Pokemon.Models;

namespace dotnettest.Pokemon.Data
{
    public class PokemonContext : DbContext
    {
        public DbSet<Species> Species => Set<Species>();
        public DbSet<m.Pokemon> Pokemons => Set<m.Pokemon>();
        public DbSet<Trainer> Trainers => Set<Trainer>();

        protected readonly IConfiguration _configuration;

        public PokemonContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase"));
        }
    }
}