using System.Threading.Tasks;
using AgroPlan.Planification.Core.Aggregate;

namespace AgroPlan.Planification.Core.Interfaces
{
    public interface ICropTypeRepostitory
    {
        Task<CropType> GetByCodeAsync(int code);
    }
}