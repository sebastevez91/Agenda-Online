using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaOnline.Data;
using AgendaOnline.Models;

namespace Agenda_Online.Pages.Labels
{
    public class EditModel : PageModel
    {
        private readonly AgendaOnline.Data.AgendaDbContext _context;

        public EditModel(AgendaOnline.Data.AgendaDbContext context)
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

            var label =  await _context.Label.FirstOrDefaultAsync(m => m.LabelId == id);
            if (label == null)
            {
                return NotFound();
            }
            Label = label;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Label).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabelExists(Label.LabelId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LabelExists(int id)
        {
            return _context.Label.Any(e => e.LabelId == id);
        }
    }
}
