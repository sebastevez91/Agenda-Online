using System.ComponentModel.DataAnnotations;

namespace AgendaOnline.Models
{
    public class Label
    {
        [Key]
        public int LabelId { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required, StringLength(50)]
        public string LabelName { get; set; } = string.Empty;

        public ICollection<LabelContact> LabelsContact { get; set; } = new List<LabelContact>();
    }
}

