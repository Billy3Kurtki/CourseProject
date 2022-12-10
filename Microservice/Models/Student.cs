using System.Collections.ObjectModel;

namespace Microservice.Models
{
    public class Student: UserBase
    {
        public int Id { get; }
        public string fullName { get; }
        public string Login { get; }
        public string Password { get; }
        public string Email { get; }
        public DateTime Birthdate { get; }
        private Group group;
        private IReadOnlyDictionary<Discipline, int> scores;
        public Student(int Id, string fullName, string Login, string Password, string Email, DateTime Birthdate, IReadOnlyDictionary<Discipline, int> scores)
        {
            this.Id = Id;
            this.fullName = fullName;
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
            this.Birthdate = Birthdate;
            this.scores = scores;
        }
        public bool admissionToExams() {
            foreach (var item in scores)
            {
                if (item.Value < 60)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
