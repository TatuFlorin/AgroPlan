using MediatR;
using System;
using AgroPlan.Planification.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace AgroPlan.Planification.Api.Application.Commands
{
    public sealed class UnregisterCropCommand : IRequest<bool>
    {
        public UnregisterCropCommand(Guid planificationId, Guid cropId)
        {
            PlanificationId = planificationId;
            CropId = cropId;
        }

        public Guid PlanificationId { get; set; }
        public Guid CropId { get; set; }

        internal class UnregisterCropHandler : IRequestHandler<UnregisterCropCommand, bool>
        {

            private readonly IPlanificationRepository _repo;
            private readonly ILogger<UnregisterCropHandler> _log;

            public UnregisterCropHandler(
                IPlanificationRepository repo,
                ILogger<UnregisterCropHandler> log
            )
            {
                _repo = repo;
                _log = log;
            }

            public async Task<bool> Handle(UnregisterCropCommand request, CancellationToken cancellationToken)
            {
                var planification = await _repo.GetByIdAsync(request.CropId);

                _ = planification ?? throw new NullReferenceException("Planification with this id doesn't extst!");

                planification.RemoveCrop(request.CropId);

                _log.LogInformation("-> Canceling Crop - Crop: {@Crop} is unregistred from {@Planification}", request.CropId, planification.Id);

                await _repo.Update(planification);

                return await _repo.Uow.SaveChangesEventsAsync(cancellationToken);
            }
        }
    }
}