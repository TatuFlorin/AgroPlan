using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using core = AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public interface IPropertyQueryRepository
        : IQueryRepository<PropertyDto, Guid>
        {
            Task<IEnumerable<ListPropertyDto>> GetByOwnerId(string ownerId);
            Task<IEnumerable<ListPropertyDto>> GetAllAsync();

        }
}