namespace Microservice.Models
{
    public class Answer
    {
        public int Id { get; }
        public string Title { get; }
        public bool isRight { get; }

        public Answer(int id, string title, bool isRight)
        {
            Id = id;
            Title = title;
            this.isRight = isRight;
        }
    }
}
