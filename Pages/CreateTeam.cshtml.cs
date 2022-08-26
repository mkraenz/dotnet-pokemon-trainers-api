using System.Security.Claims;

using dotnettest.Pokemon.Controllers;
using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnettest.Pages
{
    public class CreateTeamModel : PageModel
    {
        public Guid TrainerId { get; set; }

        [BindProperty]
        public CreateTeamDto Team { get; set; } = new CreateTeamDto();

        private readonly TeamController _teamController;
        private readonly TrainerService _trainers;
        private readonly ILogger<CreateTeamModel> _logger;

        public CreateTeamModel(TeamController teamController, TrainerService trainers, ILogger<CreateTeamModel> logger)
        {
            _teamController = teamController;
            _trainers = trainers;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            string subjectId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Pokemon.Models.Trainer? trainer = _trainers.GetByOwner(Guid.Parse(subjectId), true);
            if (trainer is null)
            {
                throw new Exception($"This should not have happened. A user without Trainer was found. SubjectId: {subjectId}");
            }
            TrainerId = trainer.Id;
            return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // refetching trainer id because we do not get the same class instance as on the previous GET request (http is stateless)
            string subjectId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Pokemon.Models.Trainer? trainer = _trainers.GetByOwner(Guid.Parse(subjectId), true);
            bool isUnauthorized = Team.TrainerId != trainer?.Id;
            if (isUnauthorized)
            {
                _logger.LogWarning("User tried to create a team for another trainer. {subjectId}", new { subjectId });
                return RedirectToPage("Errors/Error404");
            }
            try
            {
                ActionResult<Pokemon.Models.Team> createdTeam = _teamController.Create(Team);
                _trainers.Update(trainer!.Id, new UpdateTrainerDto() { ActiveTeamId = createdTeam.Value!.Id });
                return RedirectToPage("./Trainer");
            }
            catch (Exception)
            {
                return Page();
            }
        }
    }
}