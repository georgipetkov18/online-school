using Microsoft.EntityFrameworkCore;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData;

public class ApplicationDbContext : DbContext
{
    public DbSet<SubjectEntity> Subjects { get; set; } = null!;
    public DbSet<LessonEntity> Lessons { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<TeacherEntity> Teachers { get; set; } = null!;
    public DbSet<StudentEntity> Students { get; set; } = null!;
    public DbSet<AdministratorEntity> Administrators{ get; set; } = null!;
    public DbSet<ClassEntity> Classes { get; set; } = null!;
    public DbSet<TimetableEntity> Timetable { get; set; } = null!;
    public DbSet<RoleEntity> Roles { get; set; } = null!;


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubjectEntity>()
            .HasMany(subject => subject.TimetableEntities)
            .WithOne(timetableEntity => timetableEntity.Subject);

        modelBuilder.Entity<LessonEntity>()
            .HasMany(lesson => lesson.TimetableEntities)
            .WithOne(timetableEntity => timetableEntity.Lesson);

        modelBuilder.Entity<UserEntity>(builder =>
        {
            builder
                .HasMany(u => u.Teachers)
                .WithOne(t => t.User);

            builder
                .HasMany(u => u.Students)
                .WithOne(s => s.User);

            builder
                .HasMany(u => u.Administrators)
                .WithOne(a => a.User);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();
        });

        modelBuilder.Entity<TeacherEntity>(builder =>
        {
            builder
                .HasMany(teacher => teacher.TimetableEntities)
                .WithOne(timetableEntity => timetableEntity.Teacher)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Subject)
                .WithMany(s => s.Teachers);
        });

        modelBuilder.Entity<StudentEntity>()
            .HasOne(student => student.Class)
            .WithMany(_class => _class.Students);

        modelBuilder.Entity<ClassEntity>()
            .HasMany(_class => _class.TimetableEntities)
            .WithOne(timetableEntity => timetableEntity.Class);

        modelBuilder.Entity<RoleEntity>()
            .HasMany(r => r.Users)
            .WithOne(u => u.Role);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SaveAuditData();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        SaveAuditData();
        return base.SaveChanges();
    }

    private void SaveAuditData()
    {
        var addedEntities = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added).ToList();
        var modifiedEntities = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Modified).ToList();

        var createdOnPropertyName = nameof(BaseEntity.CreatedOn);
        var modifiedOnPropertyName = nameof(BaseEntity.ModifiedOn);

        addedEntities.ForEach(e =>
        {
            if (e.Properties.Any(prop => prop.Metadata.Name == createdOnPropertyName))
                e.Property(createdOnPropertyName).CurrentValue = DateTime.UtcNow;
        });

        modifiedEntities.ForEach(e =>
        {
            if (e.Properties.Any(prop => prop.Metadata.Name == createdOnPropertyName))
                e.Property(createdOnPropertyName).IsModified = false;

            if (e.Properties.Any(prop => prop.Metadata.Name == modifiedOnPropertyName))
                e.Property(modifiedOnPropertyName).CurrentValue = DateTime.UtcNow;
        });
    }
}

