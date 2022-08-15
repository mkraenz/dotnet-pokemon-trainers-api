using dotnettest.Pokemon.Models;

using Microsoft.EntityFrameworkCore;

namespace dotnettest.Pokemon.Data
{
    public class PokemonContext : DbContext
    {
        public DbSet<Species> Species => Set<Species>();
        public DbSet<Trainer> Trainers => Set<Trainer>();

        // TODO check whether _configuration works too
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