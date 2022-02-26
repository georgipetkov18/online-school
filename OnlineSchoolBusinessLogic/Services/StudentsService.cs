using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository studentsRepository;

        public StudentsService(IStudentsRepository studentsRepository)
        {
            this.studentsRepository = studentsRepository;
        }

        public async Task DeleteStudentAsync(Guid studentId) => await this.studentsRepository.DeleteStudentAsync(studentId);

        public async Task<User> GetStudentAsync(Guid studentId) => await this.studentsRepository.GetStudentAsync(studentId);
    }
}
