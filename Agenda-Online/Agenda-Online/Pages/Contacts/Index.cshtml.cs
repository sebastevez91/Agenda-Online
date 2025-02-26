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
            Contact = await _context.Contact
                .Include(c => c.Users).ToListAsync();
        }
    }
}
