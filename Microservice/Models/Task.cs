using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace Microservice.Models
{
    public class Task
    {
        public int Id { get; }
        public string Title { get; }
        private IReadOnlyCollection<Answer> answers = new Collection<Answer>();
        public Task(int Id, string Title, IReadOnlyCollection<Answer> answers) {
            this.Id = Id;
            this.Title = Title;
            this.answers = answers;
        }

    }
}
