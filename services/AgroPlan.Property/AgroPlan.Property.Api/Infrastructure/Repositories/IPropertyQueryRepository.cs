using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public interface IPropertyQueryRepository<T, I>
        : IQueryRepository<T, I>
        where T : class
        {
            Task<T> GetDetails(I Id);
            Task<IEnumerable<T>> GetByOwnerId(object ownerId);
        }
}