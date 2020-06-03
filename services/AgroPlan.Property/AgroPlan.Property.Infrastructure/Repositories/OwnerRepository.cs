using System;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure{
    public class OwnerRepository : IOwnerRepository
    {

        private readonly PropertyContext _context;

        public OwnerRepository(PropertyContext context)
        {
            _context = context;
        }

        public IUnitOfWork Uow => _context;

        public async Task<Owner> GetByIdAsync(string Id)
        {
            if(string.IsNullOrEmpty(Id))
                throw new ArgumentNullException();

            var owner = await _context.Owners.FindAsync(Id);
            return owner;
        }

        public void Remove(Owner obj)
        {
            _ = obj ?? throw new ArgumentNullException();
            _context.Owners.Remove(obj);
        }

        public void Save(Owner obj)
        {
            _ = obj ?? throw new ArgumentNullException();
            _context.Owners.Attach(obj);
        }
    }
}