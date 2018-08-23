using System;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> Get(Func<T, bool> predicate);
        Task<T> Get(int id);
        IQueryable<T> GetAll();
        Task<int> Insert(T entity);
        Task<int> Remove(T entity);
        Task<int> Update(T entity);
    }
}