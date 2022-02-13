﻿using OnlineSchoolApi.InputModels;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolApi
{
    public static class Mapper
    {
        public static Subject ToSubject(this SubjectInputModel subjectInputModel)
        {
            return new Subject
            {
                Name = subjectInputModel.Name,
                Code = subjectInputModel.Code,
            };
        }

        public static Lesson ToLesson(this LessonInputModel lessonInputModel)
        {
            return new Lesson
            {
                From = lessonInputModel.From,
                DurationInMinutes = lessonInputModel.DurationInMinutes,
            };
        }

        public static Student ToStudent(this StudentInputModel studentInputModel)
        {
            return new Student
            {
                Username = studentInputModel.Username,
                Password = studentInputModel.Password,
                Email = studentInputModel.Email,
                ClassId = studentInputModel.ClassId,
            };
        }

        public static Class ToClass(this ClassInputModel classInputModel)
        {
            return new Class
            {
                Name = classInputModel.Name,
            };
        }
    }
}
