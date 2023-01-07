using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class Discipline
    {
        public IReadOnlyCollection<Test> tests = new Collection<Test>();

        public IReadOnlyCollection<Student> students = new Collection<Student>();
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdLector { get; set; }
    }
}
