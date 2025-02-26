using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AgendaOnline.Data;
using AgendaOnline.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Agenda_Online.Pages.Contacts
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
        ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Mail");
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedLabel { get; set; } = new();

        public List<Label> LabelAvailable { get; set; } = new();

        public async Task OnGetAsync()
        {
            LabelAvailable = await _context.Label.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Asignar etiquetas seleccionadas
            foreach (var etiquetaId in SelectedLabel)
            {
                _context.LabelContact.Add(new LabelContact
                {
                    ContactId = Contact.ContactId,
                    LabelId = etiquetaId
                });
            }

            _context.Contact.Add(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
