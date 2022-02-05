using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolBusinessLogic.Services;
using OnlineSchoolData;
using OnlineSchoolData.Entities;
using OnlineSchoolData.Mappers;
using OnlineSchoolData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchoolTests.RepositoryTests
{

    public class LessonRepositoryTests
    {
        private ApplicationDbContext context = null!;
        private ILessonRepository lessonRepo = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OnlineSchoolDB")
                .Options;

            this.context = new ApplicationDbContext(options);

            this.lessonRepo = new LessonRepository(this.context);
        }

        [TearDown]
        public async Task CleanUp()
        {
            await this.context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task Add_Method_Does_Indeed_Add_Instance_To_Database()
        {
            var lessonEntity = new LessonEntity
            {
                Name = "Test Name 1",
                Code = "Test Code 1",
            };
            await this.lessonRepo.AddLessonAsync(lessonEntity.ToLesson());

            Assert.That(this.context.Lessons.Count() > 0);
        }

        [Test]
        public void Add_Method_Does_Not_Add_Instance_To_Database_When_Lesson_Is_Null()
        {
            AsyncTestDelegate testDelegate = async delegate { await this.lessonRepo.AddLessonAsync(null); };
            Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
            Assert.That(this.context.Lessons.Count() == 0);
        }

        [Test]
        public void Add_Method_Does_Not_Add_Instance_To_Database_When_Name_Is_Null()
        {
            var lessonEntity = new LessonEntity
            {
                Code = "Test Code 1",
            };

            AsyncTestDelegate testDelegate = async delegate { await this.lessonRepo.AddLessonAsync(lessonEntity.ToLesson()); };
            Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
            Assert.That(this.context.Lessons.Count() == 0);
        }

        [Test]
        public void Add_Method_Does_Not_Add_Instance_To_Database_When_Code_Is_Null()
        {
            var lessonEntity = new LessonEntity
            {
                Name = "Test Name 1",
            };

            AsyncTestDelegate testDelegate = async delegate { await this.lessonRepo.AddLessonAsync(lessonEntity.ToLesson()); };
            Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
            Assert.That(this.context.Lessons.Count() == 0);
        }


        [Test]
        public async Task Delete_Method_Does_Indeed_Delete_Instance_From_Database()
        {
            var lessonEntity = new LessonEntity
            {
                Name = "Test Name 1",
                Code = "Test Code 1",
            };

            var lesson = await this.lessonRepo.AddLessonAsync(lessonEntity.ToLesson());

            await this.lessonRepo.DeleteLessonAsync(lesson.Id);

            Assert.That(this.context.Lessons.Count() == 0);
        }

        [Test]
        public void Delete_Method_Throws_Exception_When_Id_Is_Empty()
        {
            AsyncTestDelegate testDelegate = async delegate { await this.lessonRepo.DeleteLessonAsync(Guid.Empty); };
            var exception = Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
            Assert.That(exception.Message.Contains("Id cannot be empty!"));

        }

        [Test]
        public void Delete_Method_Throws_Exception_When_Id_Is_Invalid()
        {
            AsyncTestDelegate testDelegate = async delegate { await this.lessonRepo.DeleteLessonAsync(Guid.NewGuid()); };
            var exception = Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
            Assert.That(exception.Message.Contains("Lesson with the given id does not exist!"));

        }

    }
}
