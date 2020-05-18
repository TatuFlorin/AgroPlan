using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AgroPlan.Planification.Core.Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveChangesEventsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}