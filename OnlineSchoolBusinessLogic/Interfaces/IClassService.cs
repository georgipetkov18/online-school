﻿using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces;

public interface IClassService
{
    Task<Class> AddClassAsync(Class _class);

    Task<Class> GetClassAsync(Guid classId);

    Task<IEnumerable<Class>> GetAllClassesAsync();

    Task<Class> UpdateClassAsync(Guid classId, Class _class);

    Task DeleteClassAsync(Guid classId);
}