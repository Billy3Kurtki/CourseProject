using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class Lector
    {
        public int Id { get; set; }
        public IReadOnlyCollection<Discipline> disciplines = new Collection<Discipline>();
    }
}
