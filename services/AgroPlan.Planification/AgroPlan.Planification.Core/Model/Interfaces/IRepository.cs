using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroPlan.Planification.Core.Model.Interfaces
{
    public interface IRepository<T, IDType>
     where T : Entity<IDType>
    {
        Task<T> GetById(IDType id);
    }
}