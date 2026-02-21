namespace Agenda_Online.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }
        // Navigation property for related tasks
        public List<Task> Tasks { get; set; }
    }
}
