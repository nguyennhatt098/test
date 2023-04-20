using Scenario.Domain.Entities;
using Scenario.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Data.EFCore.Repositories
{
    public class OrderRepository : RepositoryBase<Order>,IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context):base(context) =>
            _context = context;


        private bool _disposed = false;

        ~OrderRepository() =>
           Dispose();

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
