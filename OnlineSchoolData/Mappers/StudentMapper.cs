using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Mappers
{
    public static class StudentMapper
    {
        public static Student ToStudent(this StudentEntity studentEntity)
        {
            return new Student
            {
                Id = studentEntity.Id,
                Email = studentEntity.Email,
                Password = studentEntity.Password,
                Username = studentEntity.Username,
                Class = new Class { Id = studentEntity.Class.Id },
            };
        }

        public static StudentEntity ToStudentEntity(this Student student)
        {
            if (student.Class is null)
            {
                return new StudentEntity
                {
                    Id = student.Id,
                    Email = student.Email,
                    Password = student.Password,
                    Username = student.Username,
                };
            }

            return new StudentEntity
            {
                Id = student.Id,
                Email = student.Email,
                Password = student.Password,
                Username = student.Username,
                Class = new ClassEntity() { Id = student.Class.Id },
            };
        }
    }
}
