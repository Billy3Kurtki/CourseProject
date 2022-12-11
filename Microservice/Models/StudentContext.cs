using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Microservice.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options)
        : base(options)
        {
        }
        public DbSet<Student> Students { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("student");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.fullName)
                    .HasMaxLength(80)
                    .HasColumnName("fullName");

                entity.Property(e => e.Login)
                    .HasMaxLength(30)
                    .HasColumnName("Login");
                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .HasColumnName("Password");
                entity.Property(e => e.Email)
                    .HasColumnName("Email");
                entity.Property(e => e.Birthdate)
                    .HasColumnName("Birthdate");
            });
        }
    }
}
