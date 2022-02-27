using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Mappers
{
    public static class UserMapper
    {
        public static User ToStudent(this StudentEntity studentEntity)
        {
            return new User
            {
                ClassId = studentEntity.ClassId,
            };
        }

        public static StudentEntity ToStudentEntity(this User student, UserEntity user)
        {
            return new StudentEntity
            {
                Id = student.Id,
                ClassId = student.ClassId!.Value,
                User = user,
            };
        }

        public static TeacherEntity ToTeacherEntity(this User teacher, UserEntity user)
        {
            return new TeacherEntity
            {
                Id = teacher.Id,
                SubjectId = teacher.SubjectId!.Value,
                User = user,
            };
        }

        public static UserEntity ToUserEntity(this User user, RoleEntity role)
        {
            return new UserEntity
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Role = role,
            };
        }

        public static User ToUser(this UserEntity userEntity)
        {
            return new User
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleName = userEntity.Role.Name,
            };
        }
    }
}
