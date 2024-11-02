using EmpSystem.Infrastructure.DatabaseContext;
using EmpSystem.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace EmpSystem.Infrastructure.Repositories
{
    public class EmpSystemGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EmploymentSystemDBContext _empSysDbContext;
        public EmpSystemGenericRepository(EmploymentSystemDBContext userDbContext)
        {
            _empSysDbContext = userDbContext;
        }
        public T AddRecord(T entity)
        {
            _empSysDbContext.Add(entity);
            _empSysDbContext.SaveChanges();
            return entity;
        }


        public IEnumerable<T> FindWhere(Expression<Func<T, bool>> expression)
        {
            return _empSysDbContext.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _empSysDbContext.Set<T>().ToList();
        }

        public T UpdateRecord(T entity)
        {
            _empSysDbContext.Set<T>().Update(entity);
            _empSysDbContext.SaveChanges();
            return entity;
        }

        public void DeleteRecord(T entity)
        {
            _empSysDbContext.Remove(entity);
            _empSysDbContext.SaveChanges() ;
        }

        public int DeleteRecord(Expression<Func<T, bool>> expression)
        {
            _empSysDbContext.Remove(expression);
            return _empSysDbContext.SaveChanges();
        }
    }
}
