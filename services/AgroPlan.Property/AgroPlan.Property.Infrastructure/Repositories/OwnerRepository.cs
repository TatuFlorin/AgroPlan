using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using core = AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure{
    public class OwnerRepository : IOwnerRepository
    {

        private readonly PropertyContext _context;

        public OwnerRepository(PropertyContext context)
        {
            _context = context;
        }

        public Task<core.Owner> GetByIdAsync(string Id)
        {
            throw new System.NotImplementedException();
        }

        public void Save(core.Owner obj)
        {
            _context.Owners.Attach(obj);
        }

    }
}