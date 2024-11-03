using EmpSystem.Core.Entities;
using EmpSystem.Infrastructure.DatabaseContext;
using EmpSystem.Infrastructure.Interfaces;

namespace EmpSystem.Infrastructure.Repositories
{
    public class VacancyRepository : EmpSystemGenericRepository<Vacancy>, IVacancyRepository
    {
        private readonly EmploymentSystemDBContext _dbContext;
        public VacancyRepository(EmploymentSystemDBContext empSysDbContext) : base(empSysDbContext)
        {
            _dbContext = empSysDbContext;
        }
        public List<Vacancy> GetAvailableVacancies() 
        {
            var vacancies = from v in _dbContext.Vacancies
                            join va in (from vaApp in _dbContext.VacancyApplications
                                        group vaApp by vaApp.VacancyId into groupedApplications
                                        select new { VacancyId = groupedApplications.Key, AppliedCount = groupedApplications.Count() })
                                    on v.VacancyId equals va.VacancyId
                            where va.AppliedCount < v.MaxNumberOfApplications 
                                    &&v.ExpiryDate>DateTime.Now
                                    &&v.IsActive ==true
                            select v;
            return vacancies.ToList();
        }
    }
}
