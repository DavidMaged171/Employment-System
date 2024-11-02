using EmpSystem.Infrastructure.DatabaseContext;
using EmpSystem.Infrastructure.Interfaces;

namespace EmpSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmploymentSystemDBContext _employmentSystemDBContext;
        public UnitOfWork(EmploymentSystemDBContext employmentSystemDBContext) 
        {
            _employmentSystemDBContext = employmentSystemDBContext;

            vacancyRepository=new VacancyRepository(_employmentSystemDBContext);
            vacancyApplicationsRepository=new VacancyApplicationsRepository(_employmentSystemDBContext);
        }
        public IVacancyRepository vacancyRepository { get; set; }
        public IVacancyApplicationsRepository vacancyApplicationsRepository { get; set; }
    }
}
