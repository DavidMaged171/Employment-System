using EmpSystem.Core.Entities;
using EmpSystem.Infrastructure.DatabaseContext;
using EmpSystem.Infrastructure.Interfaces;

namespace EmpSystem.Infrastructure.Repositories
{
    public class VacancyApplicationsRepository : EmpSystemGenericRepository<VacancyApplications>, IVacancyApplicationsRepository
    {
        private readonly EmploymentSystemDBContext _employmentSystemDBContext;
        public VacancyApplicationsRepository(EmploymentSystemDBContext employmentSystemDBContext) : base(employmentSystemDBContext)
        {
            _employmentSystemDBContext = employmentSystemDBContext;
        }
        public int GetNumOfApplicants(int vacancyId)
        {
            return _employmentSystemDBContext.VacancyApplications.Count(x=>x.VacancyId==vacancyId);
        }
    }
}
