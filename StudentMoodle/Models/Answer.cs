namespace StudentMoodle.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool isRight { get; set; }

        public int idTask { get; set; }

        /*public Answer(int id, string title, bool isRight)
        {
            Id = id;
            Title = title;
            this.isRight = isRight;
        }*/
    }
}
