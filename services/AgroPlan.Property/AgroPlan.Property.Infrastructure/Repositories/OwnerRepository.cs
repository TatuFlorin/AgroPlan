using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Threading.Tasks;
using customEnums = AgroPlan.Property.AgroPlan.Property.Core.Enums;
using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using efcore = Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure.Repositories
{
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
                            // .Where(x => x.Id == Id)
                            // .Include(x => x.Properties)
                            //     .ThenInclude(x => x.PhysicalBlock)
                            // .FirstOrDefaultAsync();

            await _context.Entry(owner).Collection(x => x.Properties).LoadAsync();

            return owner;
        }

        public void Remove(Owner obj)
        {
            _ = obj ?? throw new ArgumentNullException();
            _context.Owners.Remove(obj);
        }

        public async Task<bool> SaveAsync(Owner obj)
        {
            _ = obj ?? throw new ArgumentNullException();

            if(!(_context.Owners.Any(x => x.Id == obj.Id))){
                _context.Add(obj);
            }

            foreach(var prop in obj.Properties)
            {

                switch(prop.EntityState)
                {
                    case customEnums.EntityState.Added:
                        _context.Entry(prop).State = efcore.EntityState.Added; break;
                    case customEnums.EntityState.Modified:
                        _context.Entry(prop).State = efcore.EntityState.Modified; break;
                    case customEnums.EntityState.Deleted:
                        _context.Entry(prop).State = efcore.EntityState.Deleted; break;
                    default:
                        break;
                }   
            }

            var DbResponse = await Uow.SaveChangesAsyncEvents(default(CancellationToken));

            return DbResponse;
        }
    }
}