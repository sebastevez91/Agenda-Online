using AgendaOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Agenda_Online.Pages.Contacts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AgendaOnline.Data.AgendaDbContext _context;
        private readonly UserManager<Users> _userManager;

        public CreateModel(AgendaOnline.Data.AgendaDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            // Obtener el usuario autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Contact.IdUser = userId;

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
