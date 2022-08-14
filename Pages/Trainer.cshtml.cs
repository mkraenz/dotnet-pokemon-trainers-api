using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Models;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnettest.Pages
{
    public class TrainerModel : PageModel
    {
        private readonly ILogger<TrainerModel> _logger;
        private readonly PokemonContext _context;

        public Trainer? Trainer { get; set; }
        public Team? Team { get; set; }

        public TrainerModel(ILogger<TrainerModel> logger, PokemonContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Trainer = _context.Trainers.OrderBy(t => t.Name).FirstOrDefault();
            List<Pokemon.Models.Pokemon> pokemons = _context.Pokemons.Take(6).ToList();
            Team = new Team()
            {
                First = pokemons[0],
                Second = pokemons[1],
                Third = pokemons[2],
                Fourth = pokemons[3],
                Fifth = pokemons[4],
                Sixth = pokemons[5],
            };
            _logger.LogInformation($"trainer {Trainer?.Name}");
        }
    }
}