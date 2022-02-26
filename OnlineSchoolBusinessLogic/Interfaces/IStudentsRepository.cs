using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces;

public interface IStudentsRepository
{
    Task<User> GetStudentAsync(Guid studentId);

    Task DeleteStudentAsync(Guid studentId);
}

