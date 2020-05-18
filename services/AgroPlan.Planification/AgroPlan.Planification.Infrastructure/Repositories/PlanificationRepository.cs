using AgroPlan.Planification.Core.Model.Interfaces;
using core = AgroPlan.Planification.Core.Model.Aggregate;
using System;
using System.Linq;
using System.Collections.Generic;
using AgroPlan.Planification.Core.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace AgroPlan.Planification.Infrastructure.Repositories
{
    public class PlanificationRepository : IPlanificationRepository
    {

        private readonly PlanContext _context;

        public PlanificationRepository(PlanContext context)
        {
            _context = context;
        }

        public IUnitOfWork Uow { get { return _context; } }

        public async Task<bool> Exist(string clientId, int year)
        {
            return await _context.Planifications
                .AnyAsync(x => x.Client.Id == clientId && x.Year == year);
        }

        //ef core don't track value objects
        public async Task<core.Planification> GetByIdAsync(Guid Id)
        {
            return await _context.Planifications.FindAsync(Id);
        }

        public core.Planification GetClientPlanification(string clientId)
        {
            return _context.Planifications.Find(clientId);
        }

        public async Task Update(core.Planification plan)
        {

            var exist = _context.Planifications.Any(x => x.Id == plan.Id);

            if (!exist)
                await _context.Planifications.AddAsync(plan);

            foreach (core.Crop crop in plan.Crops)
            {
                switch (crop.State)
                {
                    case TrackingState.Added:
                        _context.Entry(crop).State = EntityState.Added; break;
                    case TrackingState.Modified:
                        _context.Entry(crop).State = EntityState.Modified; break;
                    case TrackingState.Removed:
                        _context.Entry(crop).State = EntityState.Deleted; break;
                    default:
                        break;
                };
            }
            await Uow.SaveChangesAsync(default(CancellationToken));
        }
    }
}