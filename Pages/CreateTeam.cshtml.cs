using dotnettest.Pokemon.Controllers;
using dotnettest.Pokemon.Dtos;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnettest.Pages
{
    public class CreateTeamModel : PageModel
    {
        private readonly TeamController _teamController;

        public CreateTeamModel(TeamController teamController)
        {
            _teamController = teamController;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateTeamDto Team { get; set; } = new CreateTeamDto();

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


            return RedirectToPage("./Trainer");
        }
    }
}