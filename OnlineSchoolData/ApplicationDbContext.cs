using Microsoft.EntityFrameworkCore;
using OnlineSchoolData.Models;

namespace OnlineSchoolData
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Lesson> Lessons{ get; set; }
        public DbSet<ClassInfo> ClassInformation { get; set; }
        public DbSet<TimetableEntity> Timetable { get; set; }


        public ApplicationDbContext() : base() { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
