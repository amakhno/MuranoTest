using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class GenericRepository<T, TContext> : IGenericRepository<T> where T : Models.BaseEntity where TContext : DbContext
    {
        protected readonly TContext context;
        protected DbSet<T> entities;

        public GenericRepository(TContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public Task<T> Get(int id)
        {
            return entities.FindAsync(id);
        }

        public IQueryable<T> Get(Func<T, bool> predicate)
        {
            return entities.Where(predicate).AsQueryable();
        }
        
        public async Task<int> Insert(T entity)
        {
            entities.Add(entity);
            return context.SaveChanges();            
        }

        public async Task<int> Update(T entity)
        {
            T oldEntity = await Get(entity.Id);
            context.Entry(oldEntity).CurrentValues.SetValues(entity);
            return context.SaveChanges();
        }

        public async Task<int> Remove(T entity)
        {
            entity = await Get(entity.Id);
            entities.Remove(entity);
            return context.SaveChanges();
        }

    }
}
