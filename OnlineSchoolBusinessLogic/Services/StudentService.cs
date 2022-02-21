using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task DeleteStudentAsync(Guid studentId) => await this.studentRepository.DeleteStudentAsync(studentId);

        public async Task<Student> GetStudentAsync(Guid studentId) => await this.studentRepository.GetStudentAsync(studentId);

        public async Task<Student> UpdateStudentAsync(Guid studentId, Student student) 
            => await this.studentRepository.UpdateStudentAsync(studentId, student);
    }
}
