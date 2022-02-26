﻿using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface ISubjectsService
    {
        Task<Subject> AddSubjectAsync(Subject subject);

        Task<Subject> GetSubjectAsync(Guid subjectId);

        Task<IEnumerable<Subject>> GetAllSubjectsAsync();

        Task<Subject> UpdateSubjectAsync(Guid subjectId, Subject subject);

        Task DeleteSubjectAsync(Guid id);
    }
}