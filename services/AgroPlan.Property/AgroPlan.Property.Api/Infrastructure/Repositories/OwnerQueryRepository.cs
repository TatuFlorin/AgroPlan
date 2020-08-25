using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using AgroPlan.Property.AgroPlan.Property.Core.ValueObjects;
using Dapper;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public sealed class OwnerQueryRepository 
        : BaseRepository, IOwnerQueryRepository
    {
        public async Task<IEnumerable<OwnerDto>> GetAll()
        {
            string queryString = "SELECT * FROM Owners";
            
            var response = await _connection.QueryAsync<OwnerDto>(queryString);

            return response;
        }

        public async Task<OwnerDto> GetById(string Id)
        {
            string queryString = $"SELECT * FROM Owners WHERE Id = '{ Id }'";

            var response = await _connection.QueryFirstOrDefaultAsync<OwnerDto>(queryString);

            return response; 
        }
    }
}