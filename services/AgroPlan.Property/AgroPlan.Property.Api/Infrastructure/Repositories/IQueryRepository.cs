using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public interface IQueryRepository<T, I>
    {
        Task<T> GetById(I Id);
        Task<IEnumerable<T>> GetAll();

    }
}