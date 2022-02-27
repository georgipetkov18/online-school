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
            await this.context.Timetable.AddAsync(timetableEntry.ToTimetableEntity());
        }

        public async Task<IEnumerable<TimetableEntry>> GetCurrentDayEntriesAsync(Guid userId)
        {
            var currentDayOfWeek = DateTime.Now.DayOfWeek;

            var userEntries = await this.GetUserEntriesAsync(userId);

            return await userEntries
                .Where(t => t.Day == currentDayOfWeek)
                .Include(t => t.Subject)
                .Include(t => t.Lesson)
                .Include(t => t.Class)
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
                .FirstOrDefault(x => x.Lesson.From <= nowTimeSpan);

            return currentEntry;
        }

        public async Task<IEnumerable<TimetableEntry>> GetEntriesByDayOfWeekAsync(Guid userId, string dayOfWeek)
        {
            var userEntries = await this.GetUserEntriesAsync(userId);

            return await userEntries
                .Where(t => t.Day.ToString().ToLower() == dayOfWeek.ToLower())
                .Include(t => t.Subject)
                .Include(t => t.Lesson)
                .Include(t => t.Class)
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
            var userEntries = await this.GetUserEntriesAsync(userId);

            return await userEntries
                .Include(t => t.Subject)
                .Include(t => t.Lesson)
                .Include(t => t.Class)
                .Select(t => t.ToTimetableEntry())
                .AsNoTracking()
                .ToListAsync();
        }

        private async Task<IQueryable<TimetableEntity>> GetUserEntriesAsync(Guid userId)
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
    }
}
