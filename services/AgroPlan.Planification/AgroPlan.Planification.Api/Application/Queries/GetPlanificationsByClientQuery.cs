using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Planification.Api.Application.Dtos;
using AgroPlan.Planification.Api.Infrastructure.QueryRepositories;
using MediatR;

namespace AgroPlan.Planification.Api.Application.Queries
{
    public sealed class GetPlanificationsByClientQuery : IRequest<IEnumerable<PlanificationDto>>
    {
        public GetPlanificationsByClientQuery(string clientId)
        {
            ClientId = clientId;
        }

        public string ClientId { get; set; }

        internal class GetPlanificationsByClientHandler
            : IRequestHandler<GetPlanificationsByClientQuery, IEnumerable<PlanificationDto>>
        {
            private readonly IPlanificationQueryRepository _repo;
            public GetPlanificationsByClientHandler(IPlanificationQueryRepository repo)
            {
                _repo = repo;
            }
            public async Task<IEnumerable<PlanificationDto>> Handle(GetPlanificationsByClientQuery request, CancellationToken cancellationToken)
            {
                if (request.ClientId == string.Empty)
                    throw new ArgumentNullException("Client id can't be null.");

                var results = await _repo.GetByClientAsync(request.ClientId);

                return results;
            }
        }
    }
}