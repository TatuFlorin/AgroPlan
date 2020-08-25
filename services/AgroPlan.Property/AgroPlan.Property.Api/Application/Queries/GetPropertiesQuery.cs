using System;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories;


namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Queries
{
    public sealed class GetPropertiesQuery : IRequest<IList<ListPropertyDto>>
    {
        internal class GetPropertiesHandler : IRequestHandler<GetPropertiesQuery, IList<ListPropertyDto>>
        {

            private readonly IPropertyQueryRepository _repo;
            private readonly ILogger<GetPropertiesQuery> _logger;

            public GetPropertiesHandler(
                IPropertyQueryRepository repo
                , ILogger<GetPropertiesQuery> logger
            )
            {
                _repo = repo ?? throw new ArgumentNullException();
                _logger = logger ?? throw new ArgumentNullException();
            }

            public async Task<IList<ListPropertyDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
            {
                var properties = await _repo.GetAllAsync();
                var dtos = properties.Select(
                    x => new ListPropertyDto(x.Id, x.Surface)
                ).ToList();

                return dtos;
            }
        }
    }
}