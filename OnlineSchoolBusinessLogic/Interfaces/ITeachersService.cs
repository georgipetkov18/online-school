using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface ITeachersService
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync(string filter);
    }

}
