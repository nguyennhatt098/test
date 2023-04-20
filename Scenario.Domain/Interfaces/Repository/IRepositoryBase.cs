using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity> 
    {
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,string orderBy = "",string sortColumnDirection="", string includeProperties = "");

        public Task<TEntity> GetByID(object id);

        public Task Insert(TEntity entity);

        public Task Delete(object id);

        public Task Delete(TEntity entityToDelete);

        public Task Update(TEntity entityToUpdate);
    }
}
