using System;
using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using AgroPlan.Property.AgroPlan.Property.Core.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using core = AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Commands{
    public sealed class AddPropertyCommand : IRequest<bool>
    {

        public AddPropertyCommand(string ownerId
            , int physicalBlock
            , int parcelCode
            , float surface
            , string N_Neighbor
            , string S_Neighbor
            , string W_Neighbor
            , string E_Neighbor)
        {
            OwnerId = ownerId;
            PhysicalBlock = physicalBlock;
            ParcelCode = parcelCode;
            Surface = surface;
            this.N_Neighbor = N_Neighbor;
            this.S_Neighbor = S_Neighbor;
            this.W_Neighbor = W_Neighbor;
            this.E_Neighbor = E_Neighbor;
        }

        public string OwnerId { get; set; }
        public int PhysicalBlock { get; set; }
        public float Surface { get; set; }
        public int ParcelCode { get; set; }
        public string N_Neighbor { get; set; }
        public string S_Neighbor { get; set; }
        public string E_Neighbor { get; set; }
        public string W_Neighbor { get; set; }

        internal sealed class AddPropertyHandler : IRequestHandler<AddPropertyCommand, bool>
        {

            private readonly IOwnerRepository _repo;
            private readonly ILogger<AddPropertyCommand> _logger;

            public AddPropertyHandler(
                IOwnerRepository repo
                , ILogger<AddPropertyCommand> logger
                )
            {
                _repo = repo ?? throw new ArgumentNullException(nameof(AddPropertyCommand));
                _logger = logger ?? throw new ArgumentNullException(nameof(AddPropertyCommand));
            }

            public async Task<bool> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
            {
                if(request.OwnerId.Length != 13)
                    throw new InvalidOwnerIdException();

                var owner = await _repo.GetByIdAsync(request.OwnerId)
                    ?? throw new OwnerNotFoundException();

                owner.RegisterProperty(
                        request.PhysicalBlock,
                        request.ParcelCode,
                        new Surface(request.Surface),
                        new Neighbors(
                            request.N_Neighbor,
                            request.S_Neighbor,
                            request.W_Neighbor,
                            request.E_Neighbor
                        )
                    );       

                _logger.LogInformation("");

                _repo.Save(owner);       

                var response = await _repo.Uow.SaveChangesAsync(cancellationToken);

                return response == 1 ? true : false;   
            }
        }
    }
}