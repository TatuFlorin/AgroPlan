using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Queries
{
    public sealed class GetPropertiesByOwnerQuery : IRequest<IList<ListPropertyDto>>
    {

        public GetPropertiesByOwnerQuery(string ownerId)
            => (this.OwnerId)
            =  (ownerId);

        public string OwnerId { get; set;}

        internal class GetPropertiesByOwnerHandler : IRequestHandler<GetPropertiesByOwnerQuery, IList<ListPropertyDto>>
        {

            private readonly IPropertyQueryRepository _repo;
            private readonly ILogger<GetPropertiesByOwnerQuery> _logger;

            public GetPropertiesByOwnerHandler(IPropertyQueryRepository repo
                , ILogger<GetPropertiesByOwnerQuery> logger)
            {
                _repo = repo ?? throw new ArgumentNullException();
                _logger = logger ?? throw new ArgumentNullException();
            }

            public async Task<IList<ListPropertyDto>> Handle(GetPropertiesByOwnerQuery request, CancellationToken cancellationToken)
            {
                var properties = await _repo.GetByOwnerId(request.OwnerId);
                
                return properties.ToList();
            }
        }
    }
}