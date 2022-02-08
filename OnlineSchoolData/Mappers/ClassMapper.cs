using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Mappers
{
    public static class ClassMapper
    {
        public static Class ToClass(this ClassEntity classEntity)
        {
            return new Class
            {
                Id = classEntity.Id,
                Name = classEntity.Name,
                Students = classEntity.Students.Select(s => new Student { Id = s.Id }).ToList(),
            };
        }

        public static ClassEntity ToClassEntity(this Class _class)
        {
            if (_class.Students is null)
            {
                return new ClassEntity
                {
                    Id = _class.Id,
                    Name = _class.Name,
                };
            }

            return new ClassEntity
            {
                Id = _class.Id,
                Name = _class.Name,
                Students = _class.Students.Select(s => new StudentEntity()).ToList()
            };
        }

    }
}
