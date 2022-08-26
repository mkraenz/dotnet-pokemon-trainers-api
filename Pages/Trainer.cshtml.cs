
using System.Security.Claims;

using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;
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

        public ActionResult OnGet()
        {
            if (User.Identity is null || !User.Identity!.IsAuthenticated)
            {
                return RedirectToPage("Index");
            }

            Claim? subjectIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!;
            Claim? email = User.FindFirst(ClaimTypes.Email)!;
            Claim? name = User.FindFirst(ClaimTypes.Name)!;
            string? subjectId = subjectIdClaim?.Value;
            if (subjectId is null || email is null)
            {
                Console.WriteLine("subject id is null");
                return RedirectToPage("Errors/Error404");
            }
            Trainer = _trainers.GetByOwner(Guid.Parse(subjectId!));
            if (Trainer is null)
            {
                Console.WriteLine($"Trainer for subject id not found {subjectId}. Creating new Trainer");
                Trainer = _trainers.Create(new Trainer()
                {
                    Name = name.Value,
                    Email = email.Value,
                    OwnerId = Guid.Parse(subjectId),
                    Pokemons = new List<m.Pokemon>(),
                });
            }
            Pokemons = Trainer.Pokemons;

            Team = GetTeam(Trainer);
            return Page();
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