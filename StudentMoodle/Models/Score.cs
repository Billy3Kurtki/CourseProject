namespace StudentMoodle.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int disciplineId { get; set; }

        public int score { get; set; }

    }
}
