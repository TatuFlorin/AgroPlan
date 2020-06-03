using System;
using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Commands{
    public class UnregisterOwnerCommand : IRequest
    {
        
        public UnregisterOwnerCommand(string ownerId){
            OwnerId = ownerId;
        }

        public string OwnerId { get; set; }

        internal class UnregisterOwnerHandler : IRequestHandler<UnregisterOwnerCommand>
        {

            private readonly IOwnerRepository _repo;
            private readonly ILogger<UnregisterOwnerCommand> _logger;

            public UnregisterOwnerHandler(IOwnerRepository repo
                , ILogger<UnregisterOwnerCommand> logger)
                {
                    _repo = repo ?? throw new ArgumentNullException(nameof(UnregisterOwnerCommand));
                    _logger = logger ?? throw new ArgumentNullException(nameof(UnregisterOwnerCommand));
                }

            public async Task<Unit> Handle(UnregisterOwnerCommand request, CancellationToken cancellationToken)
            {
                if(request.OwnerId.Length != 13
                    || string.IsNullOrEmpty(request.OwnerId))
                        throw new ArgumentNullException("Must provide a valid ID.");

                var owner  = await _repo.GetByIdAsync(request.OwnerId)
                        ?? throw new OwnerNotFoundException(
                            "An Owner with mantioned ID wasn't found."
                        );

                _repo.Remove(owner);

                await _repo.Uow.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}