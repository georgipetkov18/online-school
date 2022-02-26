using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces;

public interface IStudentsRepository
{
    Task<Student> GetStudentAsync(Guid studentId);

    Task DeleteStudentAsync(Guid studentId);
}

