using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AgendaOnline.Data;
using AgendaOnline.Models;

namespace Agenda_Online.Pages.Labels
{
    public class CreateModel : PageModel
    {
        private readonly AgendaOnline.Data.AgendaDbContext _context;

        public CreateModel(AgendaOnline.Data.AgendaDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Label Label { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Label.Add(Label);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
