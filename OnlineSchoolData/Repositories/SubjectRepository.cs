using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.CustomExceptions;
using OnlineSchoolData.Entities;
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
            var subjectEntity = subject.ToSubjectEntity();
            await this.context.Subjects.AddAsync(subjectEntity);
            await this.context.SaveChangesAsync();

            return subjectEntity.ToSubject();
        }

        public async Task DeleteSubjectAsync(Guid subjectId)
        {
            var subjectEntity = await this.GetSubjectByIdAsync(subjectId);

            if (subjectEntity is null)
            {
                throw new InvalidIdException("Subject with the given id does not exist!");
            }

            this.context.Subjects.Remove(subjectEntity);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            var subjectEntities = await this.context.Subjects.AsNoTracking().ToListAsync();

            return subjectEntities.ToSubjects();
        }

        public async Task<Subject> GetSubjectAsync(Guid subjectId)
        {
            var subjectEntity = await this.GetSubjectByIdAsync(subjectId, false);

            if (subjectEntity is null)
            {
                throw new InvalidIdException("Subject with the given id does not exist!");
            }

            return subjectEntity.ToSubject();
        }

        public async Task<Subject> UpdateSubjectAsync(Guid subjectId, Subject subject)
        {
            var subjectEntity = await this.GetSubjectByIdAsync(subjectId);

            if (subjectEntity is null)
            {
                throw new InvalidIdException("Subject with the given id does not exist!");
            }

            subjectEntity.Name = subject.Name;
            subjectEntity.Code = subject.Code;

            this.context.Subjects.Update(subjectEntity);
            await this.context.SaveChangesAsync();

            return subjectEntity.ToSubject();
        }

        private async Task<SubjectEntity?> GetSubjectByIdAsync(Guid subjectId, bool tracking = true)
        {
            var query = this.context.Subjects.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(s => s.Id == subjectId);
        }
    }
}
