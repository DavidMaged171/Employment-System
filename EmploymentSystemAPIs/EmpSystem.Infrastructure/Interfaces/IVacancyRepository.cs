using EmpSystem.Core.Entities;

namespace EmpSystem.Infrastructure.Interfaces
{
    public interface IVacancyRepository:IGenericRepository<Vacancy>
    {
        public List<Vacancy> GetAvailableVacancies();
    }
}
