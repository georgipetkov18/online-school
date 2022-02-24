using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectsRepository subjectsRepository;

        public SubjectService(ISubjectsRepository subjectsRepository)
        {
            this.subjectsRepository = subjectsRepository;
        }


        public async Task<Subject> AddSubjectAsync(Subject subject) => await this.subjectsRepository.AddSubjectAsync(subject);

        public async Task DeleteSubjectAsync(Guid subjectId) => await this.subjectsRepository.DeleteSubjectAsync(subjectId);

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync() => await this.subjectsRepository.GetAllSubjectsAsync();

        public async Task<Subject> GetSubjectAsync(Guid subjectId) => await this.subjectsRepository.GetSubjectAsync(subjectId);

        public async Task<Subject> UpdateSubjectAsync(Guid subjectId, Subject subject) 
            => await this.subjectsRepository.UpdateSubjectAsync(subjectId, subject);
    }
}
