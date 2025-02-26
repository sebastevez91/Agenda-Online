using System.Diagnostics.Contracts;

namespace AgendaOnline.Models
{
    public class LabelContact
    {
        public int Id { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; } = null!;

        public int LabelId { get; set; }
        public Label Label { get; set; } = null!;
    }
}

