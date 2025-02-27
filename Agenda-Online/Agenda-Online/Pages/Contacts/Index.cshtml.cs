using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AgendaOnline.Data;
using AgendaOnline.Models;
using System.Security.Claims;

namespace Agenda_Online.Pages.Contacts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AgendaOnline.Data.AgendaDbContext _context;

        public IndexModel(AgendaOnline.Data.AgendaDbContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Contact = await _context.Contact
                .Where(c => c.IdUser == userId)
                .ToListAsync();
        }

    }
}
