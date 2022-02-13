using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.CustomExceptions;
using OnlineSchoolData.Entities;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            if (student is null)
            {
                throw new EmptyDataException(nameof(student));
            }

            var studentEntity = student.ToStudentEntity();
            await this.context.Students.AddAsync(studentEntity);
            await this.context.SaveChangesAsync();

            return studentEntity.ToStudent();
        }

        public async Task DeleteStudentAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                throw new EmptyDataException(nameof(studentId));
            }

            var studentEntity = await this.GetStudentByIdAsync(studentId);

            if (studentEntity is null)
            {
                throw new InvalidIdException("Student with the given id does not exist!", studentId);
            }

            this.context.Students.Remove(studentEntity);
            await this.context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                throw new EmptyDataException(nameof(studentId));
            }

            var studentEntity = await this.GetStudentByIdAsync(studentId, false);

            if (studentEntity is null)
            {
                throw new InvalidIdException("Student with the given id does not exist!", studentId);
            }

            return studentEntity.ToStudent();
        }

        public async Task<Student> UpdateStudentAsync(Guid studentId, Student student)
        {
            //if (student is null)
            //{
            //    throw new EmptyDataException(nameof(student));
            //}

            //if (string.IsNullOrWhiteSpace(student.Username))
            //{
            //    throw new InvalidDataProvidedException(nameof(student.Username));
            //}

            //if (string.IsNullOrWhiteSpace(student.Email))
            //{
            //    throw new InvalidDataProvidedException(nameof(student.Email));
            //}

            //if (string.IsNullOrWhiteSpace(student.Password))
            //{
            //    throw new InvalidDataProvidedException(nameof(student.Password));
            //}

            //var studentEntity = await this.GetStudentByIdAsync(studentId);

            //if (studentEntity is null)
            //{
            //    throw new InvalidIdException("Student with the given id does not exist!", studentId);
            //}

            //studentEntity.From = student.From;
            //studentEntity.DurationInMinutes = student.DurationInMinutes;

            //this.context.Students.Update(studentEntity);
            //await this.context.SaveChangesAsync();

            //return studentEntity.ToStudent();
            return null;
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
