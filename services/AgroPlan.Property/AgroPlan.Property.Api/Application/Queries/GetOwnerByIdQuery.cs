using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Queries{
    public sealed class GetOwnerByIdQuery : IRequest<OwnerDto>
    {

        public GetOwnerByIdQuery(string ownerId)
        {
            OwnerId = ownerId;
        }

        public string OwnerId { get; set; }

        internal class GetOwnerByIdHandler : IRequestHandler<GetOwnerByIdQuery, OwnerDto>
        {

            private readonly IOwnerQueryRepository _repository;
            private readonly ILogger<GetOwnerByIdQuery> _logger;

            public GetOwnerByIdHandler(IOwnerQueryRepository repository
                , ILogger<GetOwnerByIdQuery> logger)
            {
                _repository = repository;
                _logger = logger;
            }
            
            public async Task<OwnerDto> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _repository.GetById(request.OwnerId);

                return new OwnerDto(result.Id, (string)result.Name, result.TotalSurface.Value);
            }
        }
    }   
}