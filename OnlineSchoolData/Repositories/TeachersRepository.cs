using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class TeachersRepository : ITeachersRepository
    {
        private readonly ApplicationDbContext context;

        public TeachersRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync(string filter)
        {
            var teacherEntities = await this.context.Teachers
                .Include(t => t.User)
                    .ThenInclude(u => u.Role)
                .Where(t => t.User.Role.Name == Roles.Teacher && (t.User.FirstName + " " + t.User.LastName).Contains(filter))
                .AsNoTracking()
                .ToListAsync();

            return teacherEntities.Select(t => t.ToTeacher());
        }
    }
}
