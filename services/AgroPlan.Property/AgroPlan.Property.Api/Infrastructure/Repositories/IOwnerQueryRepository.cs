using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public interface IOwnerQueryRepository
        : IQueryRepository<OwnerDto, string> 
    {
        
    }
}