using System.Collections.Generic;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public sealed class OwnerQueryRepository : IOwnerQueryRepository<Owner, string>
    {
        public Task<IEnumerable<Owner>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Owner> GetById(string Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Owner> GetDetails(string Id)
        {
            throw new System.NotImplementedException();
        }
    }
}