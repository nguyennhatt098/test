using Scenario.Domain.Entities;
using Scenario.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Data.EFCore.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>,ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context):base(context) =>
            _context = context;


        private bool _disposed = false;

        ~CategoryRepository() =>
           Dispose();

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
