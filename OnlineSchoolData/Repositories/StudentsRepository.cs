using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.CustomExceptions;
using OnlineSchoolData.Entities;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly ApplicationDbContext context;

        public StudentsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task DeleteStudentAsync(Guid studentId)
        {
            var studentEntity = await this.GetStudentByIdAsync(studentId);

            if (studentEntity is null)
            {
                throw new InvalidIdException("Student with the given id does not exist!");
            }

            this.context.Students.Remove(studentEntity);
            await this.context.SaveChangesAsync();
        }

        public async Task<User> GetStudentAsync(Guid studentId)
        {
            var studentEntity = await this.GetStudentByIdAsync(studentId, false);

            if (studentEntity is null)
            {
                throw new InvalidIdException("Student with the given id does not exist!");
            }

            return studentEntity.ToStudent();
        }

        private async Task<StudentEntity?> GetStudentByIdAsync(Guid studentId, bool tracking = true)
        {
            var query = this.context.Students.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(s => s.Id == studentId);
        }
    }
}
