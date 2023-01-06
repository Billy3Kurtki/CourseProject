using Microsoft.EntityFrameworkCore;

namespace StudentMoodle.Models
{
    public class RoleContext : DbContext
    {
        public RoleContext(DbContextOptions<RoleContext> options)
        : base(options)
        {
        }

        public DbSet<RoleView> Roles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleView>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(80)
                    .HasColumnName("roleName");
            });

        }
    }
}
