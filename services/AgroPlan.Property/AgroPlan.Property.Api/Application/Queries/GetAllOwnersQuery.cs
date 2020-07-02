using MediatR;
using System.Collections.Generic;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Commands
{
    public sealed class GetAllOwnersQuery : IRequest<List<OwnerDto>>
    {
        public GetAllOwnersQuery() { }
        
        internal class GetAllOwnersHandler : IRequestHandler<GetAllOwnersQuery, List<OwnerDto>>
        {

            private readonly IOwnerQueryRepository _orepo;
            private readonly ILogger<GetAllOwnersQuery> _logg;

            public GetAllOwnersHandler(IOwnerQueryRepository orepo
                , ILogger<GetAllOwnersQuery> logg)
            {
                _orepo = orepo;
                _logg = logg;
            }

            public async Task<List<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
            {

                var owners = await _orepo.GetAll();
                
                var ownersList = new List<OwnerDto>();

                foreach(var owner in owners){
                    ownersList.Add(
                        new OwnerDto(owner.Id, (string)owner.Name, owner.TotalSurface.Value)
                    );
                }

                return ownersList;
            }
        }
    }
}