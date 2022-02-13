using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolData;
using OnlineSchoolData.CustomExceptions;
using OnlineSchoolData.Entities;
using OnlineSchoolData.Mappers;
using OnlineSchoolData.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchoolTests.RepositoryTests
{
    public class SubjectRepositoryTests
    {
        private ApplicationDbContext context = null!;
        private ISubjectRepository subjectRepo = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OnlineSchoolDB")
                .Options;

            this.context = new ApplicationDbContext(options);

            this.subjectRepo = new SubjectRepository(this.context);
        }

        [TearDown]
        public async Task CleanUp()
        {
            await this.context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task Add_Method_Does_Indeed_Add_Instance_To_Database()
        {
            var subjectEntity = new SubjectEntity
            {
                Name = "Test Name 1",
                Code = "Test Code 1",
            };
            await this.subjectRepo.AddSubjectAsync(subjectEntity.ToSubject());

            Assert.That(this.context.Subjects.Count() > 0);
        }

        [Test]
        public void Add_Method_Does_Not_Add_Instance_To_Database_When_Subject_Is_Null()
        {
            AsyncTestDelegate testDelegate = async delegate { await this.subjectRepo.AddSubjectAsync(null); };
            Assert.ThrowsAsync<EmptyDataException>(testDelegate);
            Assert.That(this.context.Subjects.Count() == 0);
        }

        [Test]
        public void Add_Method_Does_Not_Add_Instance_To_Database_When_Name_Is_Null()
        {
            var subjectEntity = new SubjectEntity
            {
                Code = "Test Code 1",
            };

            AsyncTestDelegate testDelegate = async delegate { await this.subjectRepo.AddSubjectAsync(subjectEntity.ToSubject()); };
            Assert.ThrowsAsync<EmptyDataException>(testDelegate);
            Assert.That(this.context.Subjects.Count() == 0);
        }

        [Test]
        public void Add_Method_Does_Not_Add_Instance_To_Database_When_Code_Is_Null()
        {
            var subjectEntity = new SubjectEntity
            {
                Name = "Test Name 1",
            };

            AsyncTestDelegate testDelegate = async delegate { await this.subjectRepo.AddSubjectAsync(subjectEntity.ToSubject()); };
            Assert.ThrowsAsync<EmptyDataException>(testDelegate);
            Assert.That(this.context.Subjects.Count() == 0);
        }


        [Test]
        public async Task Delete_Method_Does_Indeed_Delete_Instance_From_Database()
        {
            var subjectEntity = new SubjectEntity
            {
                Name = "Test Name 1",
                Code = "Test Code 1",
            };

            var subject = await this.subjectRepo.AddSubjectAsync(subjectEntity.ToSubject());

            await this.subjectRepo.DeleteSubjectAsync(subject.Id);

            Assert.That(this.context.Subjects.Count() == 0);
        }

        [Test]
        public void Delete_Method_Throws_Exception_When_Id_Is_Empty()
        {
            AsyncTestDelegate testDelegate = async delegate { await this.subjectRepo.DeleteSubjectAsync(Guid.Empty); };
            var exception = Assert.ThrowsAsync<EmptyDataException>(testDelegate);
            Assert.That(exception.Message.Contains("Id cannot be empty!"));

        }

        [Test]
        public void Delete_Method_Throws_Exception_When_Id_Is_Invalid()
        {
            AsyncTestDelegate testDelegate = async delegate { await this.subjectRepo.DeleteSubjectAsync(Guid.NewGuid()); };
            var exception = Assert.ThrowsAsync<InvalidIdException>(testDelegate);
            Assert.That(exception.Message.Contains("Subject with the given id does not exist!"));

        }


        [Test]
        public async Task GetAll_Method_Returns_All_Instances()
        {
            var subjectEntity1 = new SubjectEntity
            {
                Name = "Test Name 1",
                Code = "Test Code 1",
            };

            var subjectEntity2 = new SubjectEntity
            {
                Name = "Test Name 2",
                Code = "Test Code 2",
            };

            var task1 = this.subjectRepo.AddSubjectAsync(subjectEntity1.ToSubject());
            var task2 = this.subjectRepo.AddSubjectAsync(subjectEntity2.ToSubject());
            await Task.WhenAll(task1, task2);

            var subjects = await this.subjectRepo.GetAllSubjectsAsync();
            Assert.AreEqual(2, subjects.Count());
        }


        [Test]
        public void Get_Method_Throws_Exception_When_Id_Is_Invalid()
        {
            AsyncTestDelegate testDelegate = async delegate { await this.subjectRepo.GetSubjectAsync(Guid.NewGuid()); };
            var exception = Assert.ThrowsAsync<InvalidIdException>(testDelegate);
            Assert.That(exception.Message.Contains("Subject with the given id does not exist!"));
        }

        [Test]
        public async Task Get_Method_Reads_The_Correct_Subject()
        {
            var subjectEntity = new SubjectEntity
            {
                Name = "Test Name 1",
                Code = "Test Code 1",
            };

            var addedSubject = await this.subjectRepo.AddSubjectAsync(subjectEntity.ToSubject());

            var readSubject = await this.subjectRepo.GetSubjectAsync(addedSubject.Id);

            Assert.AreEqual(subjectEntity.Name, readSubject.Name);
        }


        [Test]
        public void Update_Method_Throws_Exception_When_Id_Is_Invalid()
        {
            var subjectEntity = new SubjectEntity
            {
                Id = Guid.NewGuid(),
                Name = "Test Name 1",
                Code = "Test Code 1",
            };

            AsyncTestDelegate testDelegate = async delegate 
            { 
                await this.subjectRepo.UpdateSubjectAsync(subjectEntity.Id, subjectEntity.ToSubject()); 
            };
            var exception = Assert.ThrowsAsync<InvalidIdException>(testDelegate);
            Assert.That(exception.Message.Contains("Subject with the given id does not exist!"));
        }

        [Test]
        public async Task Update_Method_Does_Indeed_Update_Subject()
        {
            var subjectEntity = new SubjectEntity
            {
                Name = "Test Name 1",
                Code = "Test Code 1",
            };

            var addedSubject = await this.subjectRepo.AddSubjectAsync(subjectEntity.ToSubject());

            subjectEntity.Id = addedSubject.Id;
            subjectEntity.Name = "Changed";

            var updatedSubject = await this.subjectRepo.UpdateSubjectAsync(subjectEntity.Id, subjectEntity.ToSubject());

            Assert.AreEqual("Changed", updatedSubject.Name);
        }

    }
}