using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class LabWork
    {
        public IReadOnlyCollection<Task> tasks = new Collection<Task>();
        public IReadOnlyCollection<Student> listPassStudents = new Collection<Student>();

        public enum Status
        {
            Open,
            Close,
            Passing,
            Сhecked
        }

        public int Id { get; }
        public string Title { get; }
        public DateTime DeadLine { get; }
        public Status status1 { get; set; }
        public string Manual { get; }

        public void closeTest()
        {
            status1 = Status.Close;
        }
        public void openTest()
        {
            status1 = Status.Open;
        }
        public void passingTest()
        {
            status1 = Status.Passing;
        }
        public void checkedTest()
        {
            status1 = Status.Сhecked;
        }
        
    }
}
