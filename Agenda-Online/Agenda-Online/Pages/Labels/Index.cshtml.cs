using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AgendaOnline.Data;
using AgendaOnline.Models;
using Microsoft.AspNetCore.Authorization;

namespace Agenda_Online.Pages.Labels
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AgendaOnline.Data.AgendaDbContext _context;

        public IndexModel(AgendaOnline.Data.AgendaDbContext context)
        {
            _context = context;
        }

        public IList<Label> Label { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Label = await _context.Label.ToListAsync();
        }
    }
}
