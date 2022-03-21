using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.CustomExceptions;
using OnlineSchoolData.Entities;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext context;

        public ClassRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Class> AddClassAsync(Class _class)
        {
            var classEntity = _class.ToClassEntity();
            await this.context.Classes.AddAsync(classEntity);
            await this.context.SaveChangesAsync();

            return classEntity.ToClass();
        }

        public async Task DeleteClassAsync(Guid classId)
        {
            var classEntity = await this.GetClassByIdAsync(classId);

            if (classEntity is null)
            {
                throw new InvalidIdException("Class with the given id does not exist!");
            }

            this.context.Classes.Remove(classEntity);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            var classEntities = await this.context.Classes.AsNoTracking().ToListAsync();

            return classEntities.Select(c => c.ToClass());
        }

        public async Task<Class> GetClassAsync(Guid classId)
        {
            var classEntity = await this.context.Classes
                .Include(c => c.Students)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == classId);

            if (classEntity is null)
            {
                throw new InvalidIdException("Class with the given id does not exist!");
            }

            return classEntity.ToClass();
        }

        public async Task<Class> UpdateClassAsync(Guid classId, Class _class)
        {
            var classEntity = await this.GetClassByIdAsync(classId);

            if (classEntity is null)
            {
                throw new InvalidIdException("Class with the given id does not exist!");
            }

            classEntity.Name = _class.Name;

            this.context.Classes.Update(classEntity);
            await this.context.SaveChangesAsync();

            return classEntity.ToClass();
        }

        private async Task<ClassEntity?> GetClassByIdAsync(Guid classId, bool tracking = true)
        {
            var query = this.context.Classes.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(c => c.Id == classId);
        }
    }
}
