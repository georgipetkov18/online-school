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

        public async Task<Student> GetStudentAsync(Guid studentId) => await this.studentsRepository.GetStudentAsync(studentId);

        public async Task<Student> UpdateStudentAsync(Guid studentId, Student student) 
            => await this.studentsRepository.UpdateStudentAsync(studentId, student);
    }
}
