using core = AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{

    public sealed class PropertyQueryRepository 
        : BaseRepository, IPropertyQueryRepository<core.Property, Guid>
    {
        public async Task<IEnumerable<core.Property>> GetAll()
        {
            string queryString = "SELECT * FROM Properties";

            var response = await _connection.QueryAsync<core.Property>(queryString);

            return response;
        }

        public async Task<core.Property> GetById(Guid Id)
        {
            string queryString = $"SELECT * FROM Properties WHERE Id = { Id }";

            var response = await _connection
                    .QueryFirstOrDefaultAsync<core.Property>(queryString);

            return response;
        }

        public async Task<IEnumerable<core.Property>> GetByOwnerId(object ownerId)
        {
            string queryString = $"SELECT * FROM Properties WHERE OwnerId = { ownerId }";

            var response = await _connection.QueryAsync<core.Property>(queryString);

            return response;
        }
    }
}