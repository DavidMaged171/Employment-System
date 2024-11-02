using EmpSystem.Core.Entities;

namespace EmpSystem.Infrastructure.Interfaces
{
    public interface IVacancyApplicationsRepository:IGenericRepository<VacancyApplications>
    {
        public int GetNumOfApplicants(int vacancyId);
    }
}
