using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dotnettest.Pages.Admin.Trainers
{
    public class EditModel : PageModel
    {
        private readonly dotnettest.Pokemon.Data.PokemonContext _context;

        public EditModel(dotnettest.Pokemon.Data.PokemonContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UpdateTrainerDto Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Trainers == null)
            {
                return NotFound();
            }

            // var trainer = await _context.Trainers.Select(t => UpdateTrainerDto.fromEntity(t))FirstOrDefaultAsync(m => m.Id == id);
            // if (trainer == null)
            // {
            //     return NotFound();
            // }
            // ViewData["ActiveTeamId"] = new SelectList(_context.Teams, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }

            // _context.Attach(Trainer).State = EntityState.Modified;

            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!TrainerExists(Trainer.Id))
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

            return RedirectToPage("./Index");
        }

        private bool TrainerExists(Guid id)
        {
            return (_context.Trainers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
