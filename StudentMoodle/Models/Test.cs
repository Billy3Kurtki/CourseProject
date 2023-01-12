using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StudentMoodle.Models
{
    public class Test
    {
        public IReadOnlyCollection<Tasks> tasks = new Collection<Tasks>();
        public IReadOnlyCollection<Student> listPassStudents = new Collection<Student>();

        public enum Status
        {
            [Display(Name = "Open")]
            Open,
            [Display(Name = "Close")]
            Close,
            [Display(Name = "Passing")]
            Passing,
            [Display(Name = "Checked")]
            Checked
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DeadLine { get; set; }
        //public string status1 { get; set; } = "Open";
        public Status status { get; set; }
        public int IdDiscipline { get; set; }


        /*public void closeTest()
        {
            status1 = "Close";
        }
        public void openTest()
        {
            status1 = "Open";
        }
        public void passingTest()
        {
            status1 = "Passing";
        }
        public void checkedTest()
        {
            status1 = "Сhecked";
        }*/
    }
}
