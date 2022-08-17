using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnettest.Pages.Admin.Teams
{

    public class IndexModel : PageModel
    {
        private readonly TrainerService _trainers;
        private readonly TeamService _teams;

        public IEnumerable<Team> Teams { get; set; } = new List<Team>();

        public IndexModel(TrainerService trainers, TeamService teams)
        {
            _trainers = trainers;
            _teams = teams;
        }

        public void OnGet()
        {
            IEnumerable<Trainer> trainers = _trainers.GetAllWithTeams();
            trainers.ToList().ForEach(trainer => trainer.Teams.ToList().ForEach(team => team.Trainer = trainer));

            Teams = (from trainer in trainers from team in trainer.Teams select team).ToList();
        }
    }
}