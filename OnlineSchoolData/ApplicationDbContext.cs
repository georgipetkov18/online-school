using Microsoft.EntityFrameworkCore;

namespace OnlineSchoolData
{
    internal class ApplicationDbContext : DbContext
    {

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
