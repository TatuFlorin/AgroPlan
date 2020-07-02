using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public interface IOwnerQueryRepository
        : IQueryRepository<Owner, string> 
    {
        
    }
}