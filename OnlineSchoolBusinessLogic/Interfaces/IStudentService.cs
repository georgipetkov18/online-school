using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces;

public interface IStudentService
{
    Task<Student> GetStudentAsync(Guid studentId);

    Task<Student> UpdateStudentAsync(Guid studentId, Student student);

    Task DeleteStudentAsync(Guid studentId);
}

