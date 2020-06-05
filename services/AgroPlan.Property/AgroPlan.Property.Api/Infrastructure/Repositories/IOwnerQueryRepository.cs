using System.Threading.Tasks;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public interface IOwnerQueryRepository<T, I> 
        : IQueryRepository<T, I> 
        where T : class
    {
        Task<T> GetDetails(I Id);
    }
}