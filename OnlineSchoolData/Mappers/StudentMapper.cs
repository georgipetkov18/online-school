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
                ClassId = studentEntity.ClassId,
            };
        }

        public static StudentEntity ToStudentEntity(this Student student)
        {
            return new StudentEntity
            {
                Id = student.Id,
                ClassId = student.ClassId,
            };
        }
    }
}
