using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Data.EFCore.Repositories
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        internal DataContext context;
        internal DbSet<TEntity> dbSet;

        public RepositoryBase(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public  IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            string sortColumnDirection = "",
            string includeProperties = "")
        {
            return Filter(filter, orderBy, includeProperties);
        }
        //public async Task<TEntity> GetOne(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    string includeProperties = "")
        //{
        //    return await Filter(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        //}
        public async Task<TEntity> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Insert(TEntity entity)
        {
           await dbSet.AddAsync(entity);
        }

        public async Task Delete(object id)
        {
            TEntity entityToDelete =await GetByID(id);
            await Delete(entityToDelete);
        }

        public async Task Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public async Task Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        private IQueryable<TEntity> Filter(
            Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            string sortColumnDirection = "",
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (sortColumnDirection == "asc")
                {
                    query = query.OrderBy(p => EF.Property<object>(p, orderBy));
                }
                else
                {
                    query = query.OrderByDescending(p => EF.Property<object>(p, orderBy));
                }
            }
            
            return query;
        }
    }
}
