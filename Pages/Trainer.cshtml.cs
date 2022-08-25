
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
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Index");
            }

            // static Claim GetSubjectIdClaim(ClaimsPrincipal user)
            // {
            //     return user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            // }

            Claim subjectIdClaim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            string? subjectId = subjectIdClaim.Value;
            if (subjectId is null)
            {
                return RedirectToPage("Errors/Error404");
            }
            Trainer = _trainers.GetByOwner(Guid.Parse(subjectId!));
            if (Trainer is null)
            {
                return RedirectToPage("Errors/Error404");
            }
            Pokemons = Trainer.Pokemons;

            _ = Trainer.Pokemons.Take(6).ToList();
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