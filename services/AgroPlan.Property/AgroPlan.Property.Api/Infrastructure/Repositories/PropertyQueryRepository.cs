using core = AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using System.Linq;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{

    public sealed class PropertyQueryRepository 
        : BaseRepository, IPropertyQueryRepository
    {
        public async Task<IEnumerable<PropertyDto>> GetAll()
        {
            string queryString = "SELECT * FROM Properties";

            var response = await _connection.QueryAsync<ListPropertyDto>(queryString);

            return null;
        }

        public async Task<IEnumerable<ListPropertyDto>> GetAllAsync()
        {
            string queryString = "SELECT * FROM Properties";

            var response = await _connection.QueryAsync<ListPropertyDto>(queryString);

            return response;
        }

        public async Task<PropertyDto> GetById(Guid Id)
        {
            string queryString = $"SELECT * FROM Properties WHERE Id='{Id}'";

            var response = await _connection
                    .QueryFirstOrDefaultAsync<PropertyDto>(queryString);

            return response;
        }

        public async Task<IEnumerable<ListPropertyDto>> GetByOwnerId(string ownerId)
        {
            string queryString = $"SELECT * FROM Properties WHERE OwnerId='{ownerId}'";

            var response = await _connection.QueryAsync<ListPropertyDto>(queryString);

            return response;
        }
    }
}