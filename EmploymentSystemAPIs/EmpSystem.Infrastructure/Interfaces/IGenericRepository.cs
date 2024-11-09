
using System.Linq.Expressions;

namespace EmpSystem.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindWhere(Expression<Func<T, bool>> expression);
        T UpdateRecord(T entity);
        T AddRecord(T entity);
        void DeleteRecord(T entity);
    }
}
