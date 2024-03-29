﻿using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Entities;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly ApplicationDbContext context;

        public TimetableRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddTimetableEntryAsync(TimetableEntry timetableEntry)
        {
            await CheckIdsValidity(timetableEntry);
            await this.context.Timetable.AddAsync(timetableEntry.ToTimetableEntity());
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TimetableEntry>> GetCurrentDayEntriesAsync(Guid userId)
        {
            var currentDayOfWeek = DateTime.Now.DayOfWeek;

            var userEntries = await this.GetTimetableEntriesAsync(userId);

            return await userEntries
                .Where(t => t.Day == currentDayOfWeek)
                .Include(t => t.Subject)
                .Include(t => t.Lesson)
                .Include(t => t.Class)
                .Include(t => t.Teacher)
                    .ThenInclude(t => t.User)
                .Select(t => t.ToTimetableEntry())
                .ToListAsync();
        }

        public async Task<TimetableEntry?> GetCurrentEntryAsync(Guid userId)
        {
            var now = DateTime.Now;
            var nowTimeSpan = new TimeSpan(now.Hour, now.Minute, now.Second);

            var currentDayEntries = await this.GetCurrentDayEntriesAsync(userId);

            if (currentDayEntries.Count() < 1)
            {
                return null;
            }

            var currentEntry = currentDayEntries
                .OrderByDescending(x => x.Lesson.From)
                .FirstOrDefault(x => x.Lesson.From <= nowTimeSpan && x.Lesson.From.Add(TimeSpan.FromMinutes(x.Lesson.DurationInMinutes)) > nowTimeSpan);

            return currentEntry;
        }

        public async Task<IEnumerable<TimetableEntry>> GetEntriesByDayOfWeekAsync(Guid userId, string dayOfWeek)
        {
            var userEntries = await this.GetTimetableEntriesAsync(userId);

            var validDayOfWeekString = char.ToUpper(dayOfWeek[0]) + dayOfWeek[1..];
            var dayOfWeekObject = Enum.Parse<DayOfWeek>(validDayOfWeekString);

            return await userEntries
                .Where(t => t.Day == dayOfWeekObject)
                .Include(t => t.Subject)
                .Include(t => t.Lesson)
                .Include(t => t.Class)
                .Include(t => t.Teacher)
                    .ThenInclude(t => t.User)
                .Select(t => t.ToTimetableEntry())
                .ToListAsync();
        }

        public async Task<TimetableEntry?> GetNextEntryAsync(Guid userId)
        {
            var now = DateTime.Now;
            var nowTimeSpan = new TimeSpan(now.Hour, now.Minute, now.Second);

            var currentDayEntries = await this.GetCurrentDayEntriesAsync(userId);

            if (currentDayEntries.Count() < 1)
            {
                return null;
            }

            var nextEntry = currentDayEntries
                .OrderBy(x => x.Lesson.From)
                .FirstOrDefault(x => x.Lesson.From > nowTimeSpan);

            return nextEntry;
        }

        public async Task<IEnumerable<TimetableEntry>> GetTimetableAsync(Guid userId)
        {
            var timetableEntries = await this.GetTimetableEntriesAsync(userId);

            return await timetableEntries
                .Include(t => t.Subject)
                .Include(t => t.Lesson)
                .Include(t => t.Class)
                .Include(t => t.Teacher)
                    .ThenInclude(t => t.User)
                .Select(t => t.ToTimetableEntry())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TimetableEntry>> GetTimetableByClassIdAsync(Guid classId)
        {

            return await this.context.Timetable
                .Where(t => t.ClassId == classId)
                .Include(t => t.Subject)
                .Include(t => t.Lesson)
                .Include(t => t.Class)
                .Include(t => t.Teacher)
                    .ThenInclude(t => t.User)
                .Select(t => t.ToTimetableEntry())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateTimetableEntryAsync(Guid timetableEntryid, TimetableEntry timetableEntry)
        {
            var entity = await this.context.Timetable.Include(e => e.Teacher).FirstOrDefaultAsync(e => e.Id == timetableEntryid);
            if (entity is null)
            {
                throw new ArgumentException("Използвано е невалидно id");
            }
            await CheckIdsValidity(timetableEntry);
            entity.SubjectId = timetableEntry.SubjectId;
            entity.LessonId = timetableEntry.LessonId;
            entity.TeacherId = timetableEntry.TeacherId;
            entity.ClassId = timetableEntry.ClassId;
            this.context.Update(entity);
            await this.context.SaveChangesAsync();
        }
        public async Task DeleteTimetableEntryAsync(Guid timetableEntryid)
        {
            var entity = await this.context.Timetable.FirstOrDefaultAsync(e => e.Id == timetableEntryid);
            if (entity is null)
            {
                throw new ArgumentException("Използвано е невалидно id");
            }

            this.context.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        private async Task<IQueryable<TimetableEntity>> GetTimetableEntriesAsync(Guid userId)
        {
            var userEntity = await this.context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (userEntity is null)
            {
                throw new ArgumentException($"User with id: {userId} does not exist");
            }

            switch (userEntity.Role.Name)
            {
                case Roles.Student:
                    var studentEntity = await this.context.Students
                        .Include(s => s.User)
                        .FirstOrDefaultAsync(s => s.User.Id == userId);

                    if (studentEntity is null)
                    {
                        throw new ArgumentException($"Student with id: {userId} does not exist");
                    }

                    return this.context.Timetable
                        .Include(t => t.Class)
                        .Where(t => t.Class.Id == studentEntity.ClassId)
                        .AsNoTracking();

                case Roles.Teacher:
                    var teacherEntity = await this.context.Teachers
                        .Include(t => t.User)
                        .FirstOrDefaultAsync(t => t.User.Id == userId);

                    if (teacherEntity is null)
                    {
                        throw new ArgumentException($"Teacher with id: {userId} does not exist");
                    }

                    return this.context.Timetable
                        .Include(t => t.Teacher)
                        .Where(t => t.Teacher != null && t.Teacher.Id == teacherEntity.Id)
                        .AsNoTracking();

                default:
                    throw new ArgumentException($"Role: {userEntity.Role.Name} does not exist");
            }

        }

        private async Task CheckIdsValidity(TimetableEntry timetableEntry)
        {
            var classExists = await this.context.Classes.AnyAsync(c => c.Id == timetableEntry.ClassId);
            if (!classExists)
            {
                throw new ArgumentException("Класът не съществува");
            }

            var subjectExists = await this.context.Subjects.AnyAsync(c => c.Id == timetableEntry.SubjectId);
            if (!subjectExists)
            {
                throw new ArgumentException("Предметът не съществува");
            }

            var lessonExists = await this.context.Lessons.AnyAsync(c => c.Id == timetableEntry.LessonId);
            if (!lessonExists)
            {
                throw new ArgumentException("Учебният час не съществува");
            }

            var teacherExists = await this.context.Teachers.AnyAsync(c => c.Id == timetableEntry.TeacherId);
            if (!teacherExists)
            {
                throw new ArgumentException("Учителят не съществува");
            }
        }
    }
}
