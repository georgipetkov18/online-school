using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface ITeachersRepository
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync(string filter);
    }

}
