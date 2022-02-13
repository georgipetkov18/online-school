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
                Username = studentEntity.Username,
                ClassId = studentEntity.ClassId,
            };
        }

        public static StudentEntity ToStudentEntity(this Student student)
        {
            return new StudentEntity
            {
                Id = student.Id,
                Email = student.Email,
                Password = student.Password,
                Username = student.Username,
                ClassId = student.ClassId,
            };
        }
    }
}
