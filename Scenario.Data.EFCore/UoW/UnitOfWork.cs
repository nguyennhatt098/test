using Scenario.Data.EFCore.Repositories;
using Scenario.Domain.Interfaces;
using System.Threading.Tasks;

namespace Scenario.Data.EFCore.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context) =>
            _context = context;

        public async Task<bool> Commit()
        {
            var success = (await _context.SaveChangesAsync()) > 0;
            
            // Possibility to dispatch domain events, etc

            return success;
        }

        public void Dispose() =>
            _context.Dispose();

        public Task Rollback()
        {
            // Rollback anything, if necessary
            return Task.CompletedTask;
        }
    }
}