using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class TeachersService : ITeachersService
    {
        private readonly ITeachersRepository teachersRepository;

        public TeachersService(ITeachersRepository teachersRepository)
        {
            this.teachersRepository = teachersRepository;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync(string filter)
            => await teachersRepository.GetAllTeachersAsync(filter);
    }
}
