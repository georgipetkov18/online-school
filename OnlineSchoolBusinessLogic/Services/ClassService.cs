using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository classRepository;

        public ClassService(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public async Task<Class> AddClassAsync(Class _class) => await this.classRepository.AddClassAsync(_class);

        public async Task DeleteClassAsync(Guid classId) => await this.classRepository.DeleteClassAsync(classId);

        public async Task<IEnumerable<Class>> GetAllClassesAsync(string filter)
            => await this.classRepository.GetAllClassesAsync(filter);

        public async Task<Class> GetClassAsync(Guid classId) => await this.classRepository.GetClassAsync(classId);

        public async Task<Class> UpdateClassAsync(Guid classId, Class _class)
            => await this.classRepository.UpdateClassAsync(classId, _class);
    }
}
