using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public interface IQueryRepository<T, I>
        where T : class
    {
        Task<T> GetById(I Id);
        Task<IEnumerable<T>> GetAll();

    }
}