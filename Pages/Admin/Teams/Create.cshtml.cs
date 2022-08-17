using dotnettest.Pokemon.Controllers;
using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnettest.Pages.Admin.Teams
{
    public class CreateModel : PageModel
    {

        [BindProperty]
        public CreateTeamDto Team { get; set; } = new CreateTeamDto();
        public ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();

        private readonly TeamController _teamController;
        private readonly TrainerService _trainers;

        public CreateModel(TeamController teamController, TrainerService trainers)
        {
            _teamController = teamController;
            _trainers = trainers;
        }

        public IActionResult OnGet()
        {
            Trainers = _trainers.GetAll().ToList();
            return Page();
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _ = _teamController.Create(Team);
            }
            catch (Exception)
            {
                return Page();
            }


            return RedirectToPage("/Trainer");
        }
    }
}