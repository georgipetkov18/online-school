using Microsoft.EntityFrameworkCore;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<LessonEntity> Lessons { get; set; } = null!;
        public DbSet<ClassInfoEntity> ClassInformation { get; set; } = null!;
        public DbSet<TimetableEntity> Timetable { get; set; } = null!;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LessonEntity>()
                .HasMany(lesson => lesson.TimetableEntities)
                .WithOne(timetableEntity => timetableEntity.Lesson)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClassInfoEntity>()
                .HasMany(classInfo => classInfo.TimetableEntities)
                .WithOne(timetableEntity => timetableEntity.ClassInfo)
                .OnDelete(DeleteBehavior.Cascade);
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
}
