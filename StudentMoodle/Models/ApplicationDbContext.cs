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
        public DbSet<Tasks> Tasks { get; set; } = default!;
        public DbSet<Answer> Answers { get; set; } = default!;
        public DbSet<Group_Discipline> Group_Disciplines { get; set; } = default!;
        public DbSet<Group> Groups { get; set; } = default!;
        public DbSet<FileModel> Files { get; set; } = default!;
        public DbSet<LabWorkandStudent> LabWorkandStudents { get; set; } = default!;

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
                    .HasColumnName("Role_id");
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

                entity.Property(e => e.IdGroup)
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
                    .HasColumnName("lector_User_idUser");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("test");

                entity.Property(e => e.Id)
                    .HasColumnName("idtest");

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.DeadLine)
                    .HasColumnName("deadLine");

                entity.Property(e => e.status)
                    .HasColumnName("status");

                entity.Property(e => e.IdDiscipline)
                    .HasColumnName("iddiscipline");
            });

            modelBuilder.Entity<LabWork>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("labwork");

                entity.Property(e => e.Id)
                    .HasColumnName("idlabwork");

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.DeadLine)
                    .HasColumnName("deadline");
                                
                entity.Property(e => e.Manual)
                    .HasColumnName("manual");

                entity.Property(e => e.status)
                    .HasColumnName("status");

                entity.Property(e => e.IdDiscipline)
                    .HasColumnName("iddiscipline");
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

            modelBuilder.Entity<Tasks>(entity =>
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

            modelBuilder.Entity<Group_Discipline>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Idgroup,
                    e.Iddiscipline
                });

                entity.ToTable("discipline_has_group");

                entity.Property(e => e.Idgroup)
                    .HasColumnName("group_idGroup");

                entity.Property(e => e.Iddiscipline)
                    .HasColumnName("discipline_iddiscipline");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("group");

                entity.Property(e => e.Id)
                    .HasColumnName("idGroup");

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.speciality)
                    .HasColumnName("speciality");
            });
            modelBuilder.Entity<FileModel>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id
                });

                entity.ToTable("file");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.Name)
                .HasColumnName("name");

                entity.Property(e => e.Path)
                .HasColumnName("path");

                entity.Property(e => e.IdStudent)
                .HasColumnName("idstudent");

                entity.Property(e => e.IdLabWork)
                .HasColumnName("idlabwork");
            });

            modelBuilder.Entity<LabWorkandStudent>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.idlabwork,
                    e.idstudent
                });

                entity.ToTable("labwork_has_student");

                entity.Property(e => e.idlabwork)
                .HasColumnName("labwork_idlabwork");

                entity.Property(e => e.idstudent)
                .HasColumnName("student_User_idUser");

                entity.Property(e => e.iddiscipline)
                .HasColumnName("discipline_iddiscipline");

                entity.Property(e => e.score)
                .HasColumnName("scorelab");
            });
        }
    }
}
