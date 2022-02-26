using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces;

public interface IStudentsService
{
    Task<User> GetStudentAsync(Guid studentId);

    Task DeleteStudentAsync(Guid studentId);
}

