using System.Collections.Generic;
using AgroPlan.Planification.Api.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace AgroPlan.Planification.Api.Infrastructure.QueryRepositories
{
    public interface IPlanificationRepository
    {
        Task<IEnumerable<PlanificationDto>> GetByClientAsync(string clientId);
    }
}