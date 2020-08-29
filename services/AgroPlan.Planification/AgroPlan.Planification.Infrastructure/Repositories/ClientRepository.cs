using System.Threading.Tasks;
using AgroPlan.Planification.Core.Aggregate;
using AgroPlan.Planification.Core.Interfaces;

namespace AgroPlan.Planification.Infrastructure.Repositories
{
    public sealed class ClientRepository : IRepository<Client, string>
    {
        private readonly PlanContext _context;

        public ClientRepository(PlanContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Client> GetById(string id)
        {
            return await _context.Clients.FindAsync(id);
        }
    }
}