using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class Lector
    {
        public int Id { get; }
        public string fullName { get; }
        public string Login { get; }
        public string Password { get; }
        public string Email { get; }
        public DateTime Birthdate { get; }
        public IReadOnlyCollection<Discipline> disciplines = new Collection<Discipline>();
        /*public Lector(int Id, string fullName, string Login, string Password, string Email, DateTime Birthdate, IReadOnlyCollection<Discipline> disciplines)
        {
            this.Id = Id;
            this.fullName = fullName;
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
            this.Birthdate = Birthdate;
            this.disciplines = disciplines;
        }*/
    }
}
