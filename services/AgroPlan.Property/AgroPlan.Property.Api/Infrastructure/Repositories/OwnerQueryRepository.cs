using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using Dapper;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories
{
    public sealed class OwnerQueryRepository 
        : BaseRepository, IOwnerQueryRepository
    {
        public async Task<IEnumerable<Owner>> GetAll()
        {
            string queryString = "SELECT * FROM Owners";
            
            var response = await _connection.QueryAsync<Owner>(queryString);

            return response;
        }

        public async Task<Owner> GetById(string Id)
        {
            string queryString = $"SELECT * FROM Owners WHERE Id = { Id }";

            var response = await _connection.QueryFirstOrDefaultAsync<Owner>(queryString);

            return response; 
        }
    }
}