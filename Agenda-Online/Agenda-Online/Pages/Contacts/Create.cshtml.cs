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

        [BindProperty]
        public Contact Contact { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedLabel { get; set; } = new List<int>();


        public List<Label> LabelAvailable { get; set; } = new();

        public async Task OnGetAsync()
        {
            LabelAvailable = await _context.Label.ToListAsync();

            // Obtener el usuario autenticado y asignarlo a Contact.IdUser
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                Contact = new Contact
                {
                    IdUser = userId
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("🔹 Entrando al método OnPostAsync...");

            // Obtener el usuario autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"🔹 Usuario autenticado: {userId}");

            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("⚠ ERROR: No se pudo obtener el ID del usuario.");
                ModelState.AddModelError(string.Empty, "No se pudo determinar el usuario.");
                return Page();
            }

            // Asignar el usuario antes de validar
            Contact.IdUser = userId;
            Contact.Users = null; // Evita que Entity Framework valide la propiedad de navegación

            Console.WriteLine($"El IdUser {Contact.IdUser} esta autenticado");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("⚠ ERROR: ModelState inválido.");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"🔹 ModelState error: {error.ErrorMessage}");
                }
                return Page();
            }

            // Guardar el contacto primero para generar el ContactId
            _context.Contact.Add(Contact);
            await _context.SaveChangesAsync(); // Importante: Esto asigna un ID válido
            Console.WriteLine($"✅ Contacto guardado con ID: {Contact.ContactId}");

            // Asignar etiquetas seleccionadas después de que ContactId tenga un valor
            foreach (var etiquetaId in SelectedLabel)
            {
                Console.WriteLine($"🔹 Asociando etiqueta {etiquetaId} al contacto {Contact.ContactId}");
                _context.LabelContact.Add(new LabelContact
                {
                    ContactId = Contact.ContactId, // Ahora sí tiene un ID válido
                    LabelId = etiquetaId
                });
            }

            await _context.SaveChangesAsync(); // Guardar etiquetas
            Console.WriteLine("✅ Todas las etiquetas fueron guardadas correctamente.");

            return RedirectToPage("./Index");
        }
    }
}
