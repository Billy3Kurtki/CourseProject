using Microsoft.EntityFrameworkCore;

namespace StudentMoodle.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<UserView> Users { get; set; } = default!;
        public DbSet<RoleView> Roles { get; set; } = default!;
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Lector> Lectors { get; set; } = default!;
        public DbSet<Discipline> Disciplines { get; set; } = default!;
        public DbSet<Test> Tests { get; set; } = default!;
        public DbSet<LabWork> LabWorks { get; set; } = default!;
        public DbSet<Score> Scores { get; set; } = default!;
        public DbSet<Task> Tasks { get; set; } = default!;
        public DbSet<Answer> Answers { get; set; } = default!;

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
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("student");

                entity.Property(e => e.Id)
                    .HasColumnName("user_iduser");

                entity.Property(e => e.iDGroup)
                    .HasColumnName("group_idGroup");
            });
            modelBuilder.Entity<Lector>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("lector");

                entity.Property(e => e.Id)
                    .HasColumnName("user_iduser");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("discipline");

                entity.Property(e => e.Id)
                    .HasColumnName("iddiscipline");

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.IdLector)
                    .HasColumnName("idlector");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("test");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasColumnName("Title");

                entity.Property(e => e.DeadLine)
                    .HasColumnName("deadLine");

                entity.Property(e => e.status1)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<LabWork>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("labwork");

                entity.Property(e => e.Id)
                    .HasColumnName("idLabWork");

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.DeadLine)
                    .HasColumnName("deadline");

                entity.Property(e => e.status1)
                    .HasColumnName("manual");

                entity.Property(e => e.status1)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("score");

                entity.Property(e => e.Id)
                    .HasColumnName("idScore");

                entity.Property(e => e.userId)
                    .HasColumnName("student_user_iduser");

                entity.Property(e => e.disciplineId)
                    .HasColumnName("discipline_iddiscipline");

                entity.Property(e => e.score)
                    .HasColumnName("score");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("task");

                entity.Property(e => e.Id)
                    .HasColumnName("idTask");

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.idTest)
                    .HasColumnName("Test_idTest");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("answeroption");

                entity.Property(e => e.Id)
                    .HasColumnName("idAnswerOption");

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.isRight)
                    .HasColumnName("isRight");

                entity.Property(e => e.idTask)
                    .HasColumnName("Task_idTask");
            });
        }
    }
}
