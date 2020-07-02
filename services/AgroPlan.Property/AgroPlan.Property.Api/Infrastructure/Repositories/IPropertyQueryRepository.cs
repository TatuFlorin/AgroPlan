using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using core = AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public interface IPropertyQueryRepository<T, I>
        : IQueryRepository<core.Property, Guid>
        {
            Task<IEnumerable<T>> GetByOwnerId(object ownerId);
        }
}