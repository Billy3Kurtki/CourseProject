using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int idTest { get; set; }
        public IReadOnlyCollection<Answer> answers = new Collection<Answer>();
        /*public Task(int Id, string Title, IReadOnlyCollection<Answer> answers) {
            this.Id = Id;
            this.Title = Title;
            this.answers = answers;
        }*/

    }
}
