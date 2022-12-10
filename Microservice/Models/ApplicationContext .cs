using Microsoft.EntityFrameworkCore;

namespace Microservice.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=root;database=Students;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
    }
}
