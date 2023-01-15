namespace StudentMoodle.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int IdStudent { get; set; }
        public int IdLabWork { get; set; }
    }
}
