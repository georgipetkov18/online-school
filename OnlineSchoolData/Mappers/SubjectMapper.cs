using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Mappers
{
    public static class SubjectMapper
    {
        public static Subject ToSubject(this SubjectEntity subjectEntity)
        {
            return new Subject
            {
                Id = subjectEntity.Id,
                Name = subjectEntity.Name,
                Code = subjectEntity.Code,
            };
        }

        public static IEnumerable<Subject> ToSubjects(this IEnumerable<SubjectEntity> subjectEntities)
        {
            IList<Subject> subjects = new List<Subject>();

            foreach (var subjectEntity in subjectEntities)
            {
                subjects.Add(subjectEntity.ToSubject());
            }

            return subjects;
        }

        public static SubjectEntity ToSubjectEntity(this Subject subject)
        {
            return new SubjectEntity
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code,
            };
        }
    }
}
