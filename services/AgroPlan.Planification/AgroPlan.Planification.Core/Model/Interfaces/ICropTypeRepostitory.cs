using System.Threading.Tasks;
using AgroPlan.Planification.Core.Model.Aggregate;

namespace AgroPlan.Planification.Core.Model.Interfaces
{
    public interface ICropTypeRepostitory
    {
        Task<CropType> GetByCodeAsync(int code);
    }
}