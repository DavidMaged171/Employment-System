
using EmpSystem.Infrastructure.Repositories;

namespace EmpSystem.UnitTest
{
    public static class UnitOfWorkSetUp
    {
        public static UnitOfWork Get()
        {
            return new UnitOfWork(new Infrastructure.DatabaseContext.EmploymentSystemDBContext());
        }
    }
}
