using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolData;
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
            Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
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
            Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
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
            Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
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
            var exception = Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
            Assert.That(exception.Message.Contains("Id cannot be empty!"));

        }

        [Test]
        public void Delete_Method_Throws_Exception_When_Id_Is_Invalid()
        {
            AsyncTestDelegate testDelegate = async delegate { await this.subjectRepo.DeleteSubjectAsync(Guid.NewGuid()); };
            var exception = Assert.ThrowsAsync<ArgumentNullException>(testDelegate);
            Assert.That(exception.Message.Contains("Subject with the given id does not exist!"));

        }

    }
}
