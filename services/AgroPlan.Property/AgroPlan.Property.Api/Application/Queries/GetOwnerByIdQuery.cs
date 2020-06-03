using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using MediatR;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Queries{
    public sealed class GetOwnerByIdCommand : IRequest<OwnerDto>
    {
        internal class GetOwnerByIdHandler : IRequestHandler<GetOwnerByIdCommand, OwnerDto>
        {
            public Task<OwnerDto> Handle(GetOwnerByIdCommand request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}