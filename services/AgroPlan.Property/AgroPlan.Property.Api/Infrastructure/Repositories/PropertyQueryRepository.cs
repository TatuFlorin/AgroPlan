using core = AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{

    public sealed class PropertyQueryRepository : IPropertyQueryRepository<core.Property, Guid>
    {
        public Task<IEnumerable<core.Property>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<core.Property> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<core.Property>> GetByOwnerId(object ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<core.Property> GetDetails(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}