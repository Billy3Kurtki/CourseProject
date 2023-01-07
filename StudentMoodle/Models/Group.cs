using System.Collections.ObjectModel;

namespace Microservice.Models
{
    public class Group
    {
        public int Id { get; }
        public string Title { get; }
        public string speciality { get; }
        private IReadOnlyCollection<Discipline> disciplines = new Collection<Discipline>();

        public Group(int id, string title, string speciality, IReadOnlyCollection<Discipline> disciplines)
        {
            Id = id;
            Title = title;
            this.speciality = speciality;
            this.disciplines = disciplines; 
        }
    }
}
