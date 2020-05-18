using AgroPlan.Planification.Core.Model.Aggregate;
using AgroPlan.Planification.Core.Model.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AgroPlan.Planification.Infrastructure.Repositories
{
    public class CropTypeRepository : ICropTypeRepostitory
    {
        private readonly PlanContext _context;
        public CropTypeRepository(PlanContext context)
        {
            _context = context;
        }

        public async Task<CropType> GetByCodeAsync(int code)
        {
            return await _context.CropTypes.SingleAsync(x => x.CropCode.Value.Equals(code));
        }

    }
}