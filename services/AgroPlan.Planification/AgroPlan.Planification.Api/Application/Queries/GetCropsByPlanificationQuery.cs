using System.Collections.Generic;
using AgroPlan.Planification.Api.Application.Dtos;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using AgroPlan.Planification.Api.Infrastructure.QueryRepositories;

namespace AgroPlan.Planification.Api.Application.Queries
{
    public sealed class GetCropsByPlanificationQuery : IRequest<IEnumerable<CropDto>>
    {
        public GetCropsByPlanificationQuery(Guid planificationId)
        {
            PlanificationId = planificationId;
        }
        public Guid PlanificationId { get; set; }

        internal class GetCropsByPlanificationHandler
            : IRequestHandler<GetCropsByPlanificationQuery, IEnumerable<CropDto>>
        {

            private readonly ICropRepository _repo;

            public GetCropsByPlanificationHandler(ICropRepository repo)
            {
                _repo = repo;
            }

            public async Task<IEnumerable<CropDto>> Handle(GetCropsByPlanificationQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetByPlanIdAsync(request.PlanificationId);
            }
        }
    }
}