using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaOnline.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required]
        public string IdUser { get; set; } = string.Empty;

        [ForeignKey("IdUser")]
        public Users Users { get; set; } = null!;

        [Required, StringLength(100)]
        public string ContactName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [EmailAddress, StringLength(150)]
        public string? Mail { get; set; }

        public string? Address { get; set; }
        public string? Photo { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string? Notes { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public ICollection<LabelContact> LabelsContact { get; set; } = new List<LabelContact>();
    }
}

