using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }


        public async Task<Subject> AddSubjectAsync(Subject subject) => await this.subjectRepository.AddSubjectAsync(subject);

        public async Task DeleteSubjectAsync(Guid id) => await this.subjectRepository.DeleteSubjectAsync(id);

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync() => await this.subjectRepository.GetAllSubjectsAsync();

        public async Task<Subject> GetSubjectAsync(Guid subjectId) => await this.subjectRepository.GetSubjectAsync(subjectId);

        public async Task<Subject> UpdateSubjectAsync(Subject subject) => await this.subjectRepository.UpdateSubjectAsync(subject);
    }
}
