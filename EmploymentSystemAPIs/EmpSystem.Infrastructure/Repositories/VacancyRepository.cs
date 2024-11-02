using EmpSystem.Core.Entities;
using EmpSystem.Infrastructure.DatabaseContext;
using EmpSystem.Infrastructure.Interfaces;

namespace EmpSystem.Infrastructure.Repositories
{
    public class VacancyRepository : EmpSystemGenericRepository<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(EmploymentSystemDBContext userDbContext) : base(userDbContext)
        {
        }
    }
}
