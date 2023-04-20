using System;
using System.Threading.Tasks;

namespace Scenario.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
        Task Rollback(); 
    }
}