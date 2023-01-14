using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string speciality { get; set; }
        public IReadOnlyCollection<Discipline> disciplines = new Collection<Discipline>();
    }
}
