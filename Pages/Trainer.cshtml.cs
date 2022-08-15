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
            // TODO use the actual team
            List<Species> pokemons = _context.Species.Take(6).ToList();
            Team = new Team()
            {
                First = pokemons.ElementAtOrDefault(0),
                Second = pokemons.ElementAtOrDefault(1),
                Third = pokemons.ElementAtOrDefault(2),
                Fourth = pokemons.ElementAtOrDefault(3),
                Fifth = pokemons.ElementAtOrDefault(4),
                Sixth = pokemons.ElementAtOrDefault(5),
            };
            _logger.LogInformation($"trainer {Trainer?.Name}");
        }
    }
}