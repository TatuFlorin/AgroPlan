using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Queries
{
    public sealed class GetPropertyDetailsQuery : IRequest<PropertyDto>
    {
        public GetPropertyDetailsQuery(Guid propertyId)
            => (this.PropertyId) = (propertyId);

        public Guid PropertyId { get; set; }

        internal class GetPropertyDetailsHandler : IRequestHandler<GetPropertyDetailsQuery, PropertyDto>
        {

            private IPropertyQueryRepository _repo;
            private ILogger<GetPropertyDetailsQuery> _logger;

            public GetPropertyDetailsHandler(IPropertyQueryRepository repo
                , ILogger<GetPropertyDetailsQuery> logger)
            {
                _repo = repo ?? throw new ArgumentNullException();
                _logger = logger ?? throw new ArgumentNullException();
            }

            public async Task<PropertyDto> Handle(GetPropertyDetailsQuery request, CancellationToken cancellationToken)
            {
                var property = await _repo.GetById(request.PropertyId);

                return property;
            }
        }
    }
}