namespace Agenda_Online.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        // Navigation properties
        public List<Contact> Contacts { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
