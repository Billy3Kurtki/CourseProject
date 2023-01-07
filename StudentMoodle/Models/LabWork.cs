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

        /*public LabWork(int Id, string Title, DateTime DeadLine, string Manual, IReadOnlyCollection<Task> tasks, IReadOnlyCollection<Student> listPassStudents)
        {
            this.Id = Id;
            this.Title = Title;
            this.DeadLine = DeadLine;
            this.tasks = tasks;
            this.listPassStudents = listPassStudents;
        }
        public LabWork(int Id, string Title, DateTime DeadLine, string Manual, Collection<Task> tasks, Collection<Student> listPassStudents, Status status1)
        {
            this.Id = Id;
            this.Title = Title;
            this.DeadLine = DeadLine;
            this.tasks = tasks;
            this.listPassStudents = listPassStudents;
            this.status1 = status1;
        }*/

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
