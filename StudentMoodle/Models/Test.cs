﻿using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace StudentMoodle.Models
{
    public class Test
    {
        public IReadOnlyCollection<Task> tasks = new Collection<Task>();
        public IReadOnlyCollection<Student> listPassStudents = new Collection<Student>();


        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DeadLine { get; set; }
        public string status1 { get; set; } = "Open";
        public int IdDiscipline { get; set; }


        public void closeTest()
        {
            status1 = "Close";
        }
        public void openTest()
        {
            status1 = "Open";
        }
        public void passingTest()
        {
            status1 = "Passing";
        }
        public void checkedTest()
        {
            status1 = "Сhecked";
        }
    }
}
