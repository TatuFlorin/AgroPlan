using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPlan.Planification.Api.Application.Dtos;
using AgroPlan.Planification.Core.Model.Aggregate;
using AgroPlan.Planification.Infrastructure.DbConnections;
using Dapper;

namespace AgroPlan.Planification.Api.Infrastructure.QueryRepositories
{
    public class CropRepository : BaseRepository, ICropRepository
    {
        public CropRepository(QueryConnectionString connString)
        : base(connString.Value) { }

        public async Task<IEnumerable<CropDto>> GetByPlanIdAsync(Guid planId)
        {
            string query = $@"SELECT crops.surface, crops.duration, croptypes.crop_name 
                FROM croptypes INNER JOIN crops 
                ON 'crops.PlanificationId' = '{ planId }' AND croptypes.Id = crops.TypeId";

            var results = await _conn.QueryAsync<CropDto>(query);

            return results;
        }
    }
}