using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AgendaOnline.Data;
using AgendaOnline.Models;

namespace Agenda_Online.Pages.Labels
{
    public class DeleteModel : PageModel
    {
        private readonly AgendaOnline.Data.AgendaDbContext _context;

        public DeleteModel(AgendaOnline.Data.AgendaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Label Label { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Label.FirstOrDefaultAsync(m => m.LabelId == id);

            if (label == null)
            {
                return NotFound();
            }
            else
            {
                Label = label;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Label.FindAsync(id);
            if (label != null)
            {
                Label = label;
                _context.Label.Remove(Label);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
