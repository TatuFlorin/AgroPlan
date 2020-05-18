using System.Threading.Tasks;
using System;
using core = AgroPlan.Planification.Core.Model.Aggregate;

namespace AgroPlan.Planification.Core.Model.Interfaces
{
    public interface IPlanificationRepository
    {
        IUnitOfWork Uow { get; }
        core.Planification GetClientPlanification(string clientId);
        Task<core.Planification> GetByIdAsync(Guid Id);
        Task Update(core.Planification plan);
        Task<bool> Exist(string clientId, int year);
    }
}