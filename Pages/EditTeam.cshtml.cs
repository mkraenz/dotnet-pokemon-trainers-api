using System.Text.Json;

using dotnettest.Pokemon.Controllers;
using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using m = dotnettest.Pokemon.Models;

namespace dotnettest.Pages
{
    public class EditTeamModel : PageModel
    {

        [BindProperty]
        public UpdateTeamDto Team { get; set; } = new UpdateTeamDto()
        {
            Name = "Updated team"
        };

        public ICollection<m.Pokemon> Pokemons { get; set; } = new List<m.Pokemon>();
        public Guid? TrainerId { get; set; }


        private readonly TeamController _teamController;
        private readonly TrainerService _trainers;

        public EditTeamModel(TeamController teamController, TrainerService trainers)
        {
            _teamController = teamController;
            _trainers = trainers;
        }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToPage("Errors/Error404");
            }
            ActionResult<Team> getTeamRes = _teamController.Get(id);
            Team? teamEntity = getTeamRes.Value;
            if (teamEntity is null)
            {
                return RedirectToPage("Errors/Error404");
            }
            Team = UpdateTeamDto.From(teamEntity);
            Trainer? trainer = _trainers.Get(teamEntity.TrainerId);
            if (trainer is null)
            {
                return RedirectToPage("Errors/Error404");
            }
            TrainerId = trainer.Id;
            // TODO handle empty pokemons
            Pokemons = trainer.Pokemons;
            return Page();
        }

        // using POST instead of PUT because html forms only support post or get
        public IActionResult OnPost(Guid? id)
        {

            if (id is null || id == Guid.Empty || !ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                ActionResult<Team> actionResult = _teamController.Update((Guid)id, Team);
                // TODO i think actionResult can fail but still return with errors like NotFound or BadRequest. would need to handle that, or maybe directly go the service instead.
                RedirectToPageResult res = RedirectToPage("Trainer", new { trainerId = actionResult.Value.TrainerId });
                Console.WriteLine(JsonSerializer.Serialize(res.RouteValues));
                return res;
            }
            catch (Exception)
            {
                return Page();
            }

        }
    }
}