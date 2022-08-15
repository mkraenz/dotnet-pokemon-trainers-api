using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc.RazorPages;

using m = dotnettest.Pokemon.Models;

namespace dotnettest.Pages
{
    public class TrainerModel : PageModel
    {
        private readonly ILogger<TrainerModel> _logger;
        private readonly TrainerService _trainers;

        public Trainer? Trainer { get; set; }
        public Team? Team { get; set; }
        public ICollection<m.Pokemon> Pokemons { get; set; } = new List<m.Pokemon>();

        public TrainerModel(ILogger<TrainerModel> logger, TrainerService trainers)
        {
            _logger = logger;
            _trainers = trainers;
        }

        public void OnGet(Guid? trainerId)
        {
            if (trainerId is null)
            {
                return;
            }
            Trainer = _trainers.Get((Guid)trainerId);
            if (Trainer is null)
            {
                return;
            }
            Pokemons = Trainer.Pokemons;

            // // TODO use the actual team
            _ = Trainer.Pokemons.Take(6).ToList();
            Team = new Team()
            {
                // First = pokemons.ElementAtOrDefault(0),
                // Second = pokemons.ElementAtOrDefault(1),
                // Third = pokemons.ElementAtOrDefault(2),
                // Fourth = pokemons.ElementAtOrDefault(3),
                // Fifth = pokemons.ElementAtOrDefault(4),
                // Sixth = pokemons.ElementAtOrDefault(5),
            };
        }
    }
}