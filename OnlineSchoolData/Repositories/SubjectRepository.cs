using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext context;

        public SubjectRepository(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public async Task<Subject> AddSubjectAsync(Subject subject)
        {
            if (subject is null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            if (string.IsNullOrWhiteSpace(subject.Name))
            {
                throw new ArgumentNullException(nameof(subject.Name));
            }

            if (string.IsNullOrWhiteSpace(subject.Code))
            {
                throw new ArgumentNullException(nameof(subject.Code));
            }

            var subjectEntity = subject.ToSubjectEntity();
            await this.context.Subjects.AddAsync(subjectEntity);
            await this.context.SaveChangesAsync();

            return subjectEntity.ToSubject();
        }

        public async Task DeleteSubjectAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id cannot be empty!");
            }

            var subjectEntity = await this.context.Subjects.FirstOrDefaultAsync(l => l.Id == id);

            if (subjectEntity is null)
            {
                throw new ArgumentNullException(nameof(subjectEntity), "Subject with the given id does not exist!");
            }

            this.context.Subjects.Remove(subjectEntity);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            var subjectEntities = await this.context.Subjects.ToListAsync();

            return subjectEntities.ToSubjects();
        }

        public async Task<Subject> GetSubjectAsync(Guid subjectId)
        {
            if (subjectId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(subjectId));
            }

            var subjectEntity = await this.context.Subjects.FirstOrDefaultAsync(l => l.Id == subjectId);

            if (subjectEntity is null)
            {
                throw new ArgumentNullException(nameof(subjectEntity));
            }

            return subjectEntity.ToSubject();
        }

        public async Task<Subject> UpdateSubjectAsync(Subject subject)
        {
            if (subject is null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            if (string.IsNullOrWhiteSpace(subject.Name))
            {
                throw new ArgumentNullException(nameof(subject.Name));
            }

            if (string.IsNullOrWhiteSpace(subject.Code))
            {
                throw new ArgumentNullException(nameof(subject.Code));
            }

            var subjectEntity = await this.context.Subjects.FirstOrDefaultAsync(l => l.Id == subject.Id);

            if (subjectEntity is null)
            {
                throw new ArgumentNullException(nameof(subjectEntity));
            }

            subjectEntity.Name = subject.Name;
            subjectEntity.Code = subject.Code;

            this.context.Subjects.Update(subjectEntity);
            await this.context.SaveChangesAsync();

            return subjectEntity.ToSubject();
        }
    }
}
