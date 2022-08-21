using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Models;

namespace dotnettest.Pages.Admin.Trainers
{
    public class IndexModel : PageModel
    {
        private readonly dotnettest.Pokemon.Data.PokemonContext _context;

        public IndexModel(dotnettest.Pokemon.Data.PokemonContext context)
        {
            _context = context;
        }

        public IList<Trainer> Trainer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Trainers != null)
            {
                Trainer = await _context.Trainers
                .Include(t => t.ActiveTeam).ToListAsync();
            }
        }
    }
}
