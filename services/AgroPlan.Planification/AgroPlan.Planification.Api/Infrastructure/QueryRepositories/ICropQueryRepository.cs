using System;
using core = AgroPlan.Planification.Core.Aggregate;
using System.Collections.Generic;
using AgroPlan.Planification.Api.Application.Dtos;
using System.Threading.Tasks;

namespace AgroPlan.Planification.Api.Infrastructure.QueryRepositories
{
    public interface ICropQueryRepository
    {
        Task<IEnumerable<CropDto>> GetByPlanIdAsync(Guid planId);
        // IEnumerable<core.Crop> GetByCropTypeAsync(int cropCode);
    }
}