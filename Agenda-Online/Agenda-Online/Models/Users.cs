using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace AgendaOnline.Models
{
    public class Users : IdentityUser
    {
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public string? ProfilePhoto { get; set; }

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public ICollection<Label> Labels { get; set; } = new List<Label>();
    }
}

