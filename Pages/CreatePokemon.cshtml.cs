using System.Security.Claims;

using dotnettest.Pokemon.Controllers;
using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnettest.Pages
{
    public class CreatePokemonModel : PageModel
    {
        public Guid Teamid { get; set; }

        [BindProperty]
        public CreatePokemonDto Pokemon { get; set; } = new CreatePokemonDto();

        private readonly PokemonController _pokemonController;
        private readonly TrainerService _trainers;
        private readonly ILogger<CreatePokemonModel> _logger;

        public CreatePokemonModel(PokemonController pokemonController, TrainerService trainers, ILogger<CreatePokemonModel> logger)
        {
            _pokemonController = pokemonController;
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
            Pokemon.TrainerId = trainer.Id;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string subjectId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Pokemon.Models.Trainer? trainer = _trainers.GetByOwner(Guid.Parse(subjectId), true);
            bool isUnauthorized = Pokemon.TrainerId != trainer?.Id;
            if (isUnauthorized)
            {
                _logger.LogWarning("User tried to create a Pokemon for another trainer. {subjectId}", new { subjectId });
                return RedirectToPage("Errors/Error404");
            }
            try
            {
                ActionResult<Pokemon.Models.Pokemon> createdTeam = await _pokemonController.CreateAsync(Pokemon);
                return RedirectToPage("./Trainer");
            }
            catch (Exception)
            {
                return Page();
            }
        }
    }
}