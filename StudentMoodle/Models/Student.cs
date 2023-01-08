using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int IdGroup { get; set; }

        public IReadOnlyDictionary<Discipline, int> scores;
        public bool admissionToExams() {
            foreach (var item in scores)
            {
                if (item.Value < 60)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
