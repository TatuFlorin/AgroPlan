using System.Threading;
using System.Threading.Tasks;

namespace AgroPlan.Property.AgroPlan.Property.Core.Interfaces{
    public interface IUnitOfWork {

     Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
     Task<bool> SaveChangesAsyncEvents(CancellationToken cancellationToken = default);
    }
}