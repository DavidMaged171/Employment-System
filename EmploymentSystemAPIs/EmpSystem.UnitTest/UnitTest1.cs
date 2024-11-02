using EmpSystem.Application.BusinessInterfaces;
using EmpSystem.Application.BusinessLogic;
using EmpSystem.Infrastructure.Repositories;

namespace EmpSystem.UnitTest
{
    public class Tests
    {
        private IVacancyProcessor _vacancyProcessor;
        [SetUp]
        public void Setup()
        {
            _vacancyProcessor = new VacancyProcessor(UnitOfWorkSetUp.Get());
        }

        [Test]
        public void TestCreateVacancy()
        {
            _vacancyProcessor.CreateNewVacancy(new Application.DTOs.Request.VacancyCreateRequest()
            {
                ExpiryDate = DateTime.Now,
                IsActive = true,
                MaxNumberOfApplicantions = 1,
                VacancyName="Software Engineer"
            });
        }
        [Test]
        public void TestUpdateVacancy()
        {
            _vacancyProcessor.UpdateVacancy(new Application.DTOs.Request.VacancyUpdateReuest()
            {
                ExpiryDate = DateTime.Now,
                IsActive = true,
                MaxNumberOfApplicantions = 1,
                VacancyName = "Software Engineer"
            });
        }
    }
}