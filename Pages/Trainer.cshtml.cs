using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc.RazorPages;

using m = dotnettest.Pokemon.Models;

namespace dotnettest.Pages
{
    public class TrainerModel : PageModel
    {
        private readonly TrainerService _trainers;
        private readonly TeamService _teams;

        public Trainer? Trainer { get; set; }
        public Team? Team { get; set; }
        public ICollection<m.Pokemon> Pokemons { get; set; } = new List<m.Pokemon>();

        public TrainerModel(TrainerService trainers, TeamService teams)
        {
            _trainers = trainers;
            _teams = teams;
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

            _ = Trainer.Pokemons.Take(6).ToList();
            Team = GetTeam(Trainer);
        }

        private Team GetTeam(Trainer trainer)
        {
            return trainer.ActiveTeamId is null
                ? new Team()
                {
                    TrainerId = trainer.Id,
                    Name = "New Team"
                }
                : _teams.Get((Guid)trainer.ActiveTeamId)!;
        }
    }
}