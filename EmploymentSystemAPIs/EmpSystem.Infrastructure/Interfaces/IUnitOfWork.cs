
namespace EmpSystem.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public IVacancyRepository vacancyRepository { get; }
    }
}
