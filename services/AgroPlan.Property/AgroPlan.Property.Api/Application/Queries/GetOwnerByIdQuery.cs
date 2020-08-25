using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories;
using AgroPlan.Property.AgroPlan.Property.Core.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Queries{
    public sealed class GetOwnerByIdQuery : IRequest<OwnerDto>
    {

        public GetOwnerByIdQuery() { }
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

                if(request.OwnerId is null || request.OwnerId == "0")
                        throw new InvalidOwnerIdException();

                var result = await _repository.GetById(request.OwnerId);

                if(result is null)
                        throw new OwnerNotFoundException();

                return result;
            }
        }
    }   
}