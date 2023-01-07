using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class Discipline
    {
        private IReadOnlyCollection<Test> tests = new Collection<Test>();

        private IReadOnlyCollection<Student> students = new Collection<Student>();
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdLector { get; set; }
    }
}
