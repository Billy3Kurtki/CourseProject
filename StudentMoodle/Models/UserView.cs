namespace StudentMoodle.Models
{
    public class UserView
    {
        public int Id { get; set; }
        public string fullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public int? RoleId { get; set; }
    }
}
