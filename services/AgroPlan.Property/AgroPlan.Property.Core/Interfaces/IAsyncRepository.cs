using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroPlan.Property.AgroPlan.Property.Core.Interfaces{
    public interface IAsyncRepository<T, I> 
        where T : class 
    {
        Task<T> GetByIdAsync(I Id);

        void Save(T obj);
    }
}