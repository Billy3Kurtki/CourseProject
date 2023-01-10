using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class Test
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

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DeadLine { get; set; }
        public Status status1 { get; set; }

        /*public Test(int Id, string Title, DateTime DeadLine, IReadOnlyCollection<Task> tasks)
        {
           this.Id = Id;
           this.Title = Title;
           this.DeadLine = DeadLine;
           this.tasks = tasks;
        }
        public Test(int Id, string Title, DateTime DeadLine, IReadOnlyCollection<Student> listPassStudents, IReadOnlyCollection<Task> tasks, Status status1)
        {
            this.Id = Id;
            this.Title = Title;
            this.DeadLine = DeadLine;
            this.status1 = status1;
            this.listPassStudents = listPassStudents;
            this.tasks = tasks;
        }*/

        public void closeTest() {
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
