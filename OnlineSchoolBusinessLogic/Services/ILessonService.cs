﻿using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services;

public interface ILessonService
{
    Task<Lesson> AddLessonAsync(Lesson lesson);

    Task<Lesson> GetLessonAsync(Guid lessonId);

    Task<Lesson> UpdateLessonAsync(Guid lessonId, Lesson lesson);

    Task DeleteLessonAsync(Guid id);
}

