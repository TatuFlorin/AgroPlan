using MediatR;
using System.Collections.Generic;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Commands
{
    public sealed class GetAllOwnersQuery : IRequest<IEnumerable<OwnerDto>>
    {
        public GetAllOwnersQuery() { }
        
        internal class GetAllOwnersHandler : IRequestHandler<GetAllOwnersQuery, IEnumerable<OwnerDto>>
        {

            private readonly IOwnerQueryRepository _orepo;
            private readonly ILogger<GetAllOwnersQuery> _logg;

            public GetAllOwnersHandler(IOwnerQueryRepository orepo
                , ILogger<GetAllOwnersQuery> logg)
            {
                _orepo = orepo;
                _logg = logg;
            }

            public async Task<IEnumerable<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
            {

                var owners = await _orepo.GetAll();
                return owners;
          
            }
        }
    }
}