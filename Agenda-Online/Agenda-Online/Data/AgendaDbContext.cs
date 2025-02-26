using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AgendaOnline.Models;

namespace AgendaOnline.Data
{
    public class AgendaDbContext : IdentityDbContext<Users>
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options) { }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Label> Label { get; set; }
        public DbSet<LabelContact> LabelContact { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relación muchos a muchos
            modelBuilder.Entity<LabelContact>()
                .HasOne(ce => ce.Contact)
                .WithMany(c => c.LabelsContact)
                .HasForeignKey(ce => ce.ContactId);

            modelBuilder.Entity<LabelContact>()
                .HasOne(ce => ce.Label)
                .WithMany(e => e.LabelsContact)
                .HasForeignKey(ce => ce.LabelId);
        }
    }
}

