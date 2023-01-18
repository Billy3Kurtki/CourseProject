namespace StudentMoodle.Models
{
    public class LabWorkandStudent
    {
        public int idlabwork { get; set; }
        public int idstudent { get; set; }
        public int iddiscipline { get; set; }
        public int score { get; set; }
        public DateTime passDate { get; set; }
        public int idlector { get; set; }
    }
}
