using System;
using MediatR;
using AgroPlan.Planification.Core.Model.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using AgroPlan.Planification.Core.Model.ValueObjects;

namespace AgroPlan.Planification.Api.Application.Commands
{
    public class RegisterCropCommand : IRequest<bool>
    {
        public RegisterCropCommand() { }

        public RegisterCropCommand(int cropCode, Guid planificationId, float surface
        , int physicalBlockCode, int parcelCode)
        {
            CropCode = cropCode;
            PlanificationId = planificationId;
            Surface = surface;
            PhysicalBlockCode = physicalBlockCode;
            ParcelCode = parcelCode;
        }

        public int CropCode { get; set; }
        public Guid PlanificationId { get; set; }
        public float Surface { get; set; }
        public int PhysicalBlockCode { get; set; }
        public int ParcelCode { get; set; }

        internal class RegisterCropHandler : IRequestHandler<RegisterCropCommand, bool>
        {

            private readonly IPlanificationRepository _repo;
            private readonly ICropTypeRepostitory _cRepo;
            private readonly ILogger _log;
            public RegisterCropHandler(
                IPlanificationRepository repo
                , ILogger<RegisterCropHandler> log
                , ICropTypeRepostitory cRepo
            )
            {
                _repo = repo ?? throw new ArgumentNullException(nameof(repo));
                _log = log ?? throw new ArgumentNullException(nameof(log));
                _cRepo = cRepo ?? throw new ArgumentNullException(nameof(cRepo));
            }

            public async Task<bool> Handle(RegisterCropCommand request, CancellationToken cancellationToken)
            {
                var planification = await _repo.GetByIdAsync(request.PlanificationId);
                _ = planification ?? throw new NullReferenceException("Planification wasn't found or dont't exist!");

                var croptype = await _cRepo.GetByCodeAsync(request.CropCode);
                _ = croptype ?? throw new NullReferenceException("There is no crop type with this code!");

                planification.AddCrop(croptype
                    , new Surface(request.Surface)
                    , new Code(request.PhysicalBlockCode)
                    , new Code(request.ParcelCode));

                await _repo.Update(planification);

                _log.LogInformation("-> Registring Crop -> Crop : {@CropTypeCod} - {@Surface}", request.CropCode, request.Surface);

                return await _repo.Uow.SaveChangesEventsAsync();
            }
        }
    }
}