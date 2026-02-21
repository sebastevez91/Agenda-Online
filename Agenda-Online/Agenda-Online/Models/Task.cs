using System.ComponentModel.DataAnnotations;

namespace Agenda_Online.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre de tarea")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Descripción de tarea")]
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }

        // Foreign key to Contact (optional)
        public int? ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
