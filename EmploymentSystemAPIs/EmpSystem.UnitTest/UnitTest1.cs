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
            _vacancyProcessor = new VacancyProcessor(new UnitOfWork(new Infrastructure.DatabaseContext.EmploymentSystemDBContext()));
        }

        [Test]
        public void Test1()
        {
            _vacancyProcessor.CreateNewVacancy(new Application.DTOs.Request.VacancyCreateRequest()
            {
                ExpiryDate = DateTime.Now,
                IsActive = true,
                MaxNumberOfApplicantions = 1,
                VacancyName="Software Engineer"
            });
        }
    }
}