using System.Collections.ObjectModel;

namespace Microservice.Models
{
    public class Discipline
    {
        private IReadOnlyCollection<Test> tests = new Collection<Test>();
        private Lector lector;
        private IReadOnlyCollection<Student> students = new Collection<Student>();
        public int Id { get; }
        public string Title { get; }
        
        public Discipline(int Id, string Title, Lector lector, IReadOnlyCollection<Test> tests, IReadOnlyCollection<Student> students)
        {
            this.Id = Id;
            this.Title = Title;
            this.lector = lector;
            this.tests = tests;
            this.students = students;
        }
    }
}
