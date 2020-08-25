using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System;
using Microsoft.Extensions.Logging;
using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using AgroPlan.Property.AgroPlan.Property.Core.Exceptions;
using System.Linq;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Commands{
    public sealed class EliminatePropertyCommand : IRequest<bool>
    {

        public EliminatePropertyCommand(string ownerId, Guid propertyId){
            OwnerId = ownerId;
            PropertyId = propertyId;
        }

        public string OwnerId { get; set; }
        public Guid PropertyId { get; set; }

        internal sealed class EliminatePropertyHandler : IRequestHandler<EliminatePropertyCommand, bool>
        {

            private readonly IOwnerRepository _repo;
            private readonly ILogger<EliminatePropertyCommand> _logger;

            public EliminatePropertyHandler(IOwnerRepository repo
                , ILogger<EliminatePropertyCommand> logger)
                {
                    _repo = repo ?? throw new ArgumentNullException(nameof(EliminatePropertyCommand));
                    _logger = logger ?? throw new ArgumentNullException(nameof(EliminatePropertyCommand));
                }

            public async Task<bool> Handle(EliminatePropertyCommand request, CancellationToken cancellationToken)
            {
                if(string.IsNullOrEmpty(request.OwnerId) || request.OwnerId.Length != 13)
                    throw new ArgumentNullException();

                var owner = await _repo.GetByIdAsync(request.OwnerId)
                    ?? throw new OwnerNotFoundException();

                var property = owner.Properties.FirstOrDefault(x => x.Id == request.PropertyId)
                    ?? throw new PropertyNotExistException();

                _logger.LogInformation("");

                owner.UnregisterProperty(request.PropertyId);

                var response = await _repo.SaveAsync(owner);

                return response;        
            }
        }
    }
}