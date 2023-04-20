using Scenario.Domain.Entities;
using Scenario.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Data.EFCore.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>,ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context):base(context) =>
            _context = context;


        private bool _disposed = false;

        ~CustomerRepository() =>
           Dispose();

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
