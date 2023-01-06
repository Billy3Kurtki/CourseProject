using Microsoft.EntityFrameworkCore;

namespace StudentMoodle.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
        : base(options)
        {
        }
        public DbSet<UserView> Users { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserView>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("iduser");

                entity.Property(e => e.fullName)
                    .HasMaxLength(80)
                    .HasColumnName("fullName");

                entity.Property(e => e.Login)
                    .HasMaxLength(30)
                    .HasColumnName("login");
                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .HasColumnName("password");
                entity.Property(e => e.Email)
                    .HasColumnName("email");
                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate");
                entity.Property(e => e.RoleId)
                    .HasColumnName("roleid");
            });
        }
    }
}
