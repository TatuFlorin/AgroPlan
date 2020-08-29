using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AgroPlan.Planification.Api.Application.Dtos;
using AgroPlan.Planification.Infrastructure.DbConnections;
using Dapper;
using Npgsql;

namespace AgroPlan.Planification.Api.Infrastructure.QueryRepositories
{
    public class PlanificationQueryRepository : BaseRepository, IPlanificationQueryRepository
    {
        public PlanificationQueryRepository(QueryConnectionString connString)
        : base(connString.Value)
        {
        }

        public async Task<IEnumerable<PlanificationDto>> GetByClientAsync(string clientId)
        {
            string query = $@"SELECT planification.client_id AS cnp, planification.planification_year AS year,
            planification.surface, CONCAT(clients.first_name, ' ', clients.last_name) AS fullname FROM clients
            INNER JOIN planification ON client_id = '{ clientId }'";

            var results = await _conn.QueryAsync<PlanificationDto>(query);
            return results;

        }
    }
}
