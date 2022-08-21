using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Models;

namespace dotnettest.Pages.Admin.Trainers
{
    public class CreateModel : PageModel
    {
        private readonly dotnettest.Pokemon.Data.PokemonContext _context;

        public CreateModel(dotnettest.Pokemon.Data.PokemonContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ActiveTeamId"] = new SelectList(_context.Teams, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Trainers == null || Trainer == null)
            {
                return Page();
            }

            _context.Trainers.Add(Trainer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
