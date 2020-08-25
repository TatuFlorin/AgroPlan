using System;
using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Commands{
    public class RegisterOwnerCommand : IRequest<bool>
    {
        public RegisterOwnerCommand() {}

        public RegisterOwnerCommand(string id, string firstName, string lastName){
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Id {get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}        

        internal class RegisterOwnerHandler : IRequestHandler<RegisterOwnerCommand, bool>
        {

            private readonly IOwnerRepository _repo;
            private readonly ILogger _logger;

            public RegisterOwnerHandler(
                IOwnerRepository repo
                , ILogger<RegisterOwnerCommand> logger)
            {
                _repo = repo ?? throw new ArgumentNullException(nameof(RegisterOwnerCommand));
                _logger = logger ?? throw new ArgumentNullException(nameof(RegisterOwnerCommand));
            }

            public async Task<bool> Handle(RegisterOwnerCommand request, CancellationToken cancellationToken)
            {
                if(string.IsNullOrEmpty(request.Id)
                || string.IsNullOrEmpty(request.FirstName)
                || string.IsNullOrEmpty(request.LastName))
                    throw new ArgumentNullException(
                        "One of the fiels is empty!"
                    );

                if(request.Id.Length != 13)
                        throw new InvalidOwnerIdException();

                var response = await _repo.SaveAsync(Owner.Create
                (
                    request.Id,
                    request.FirstName,
                    request.LastName
                ));

                _logger.LogInformation("[{0} : {2}] -> OWNER : ID {1} - WAS CREATED!"
                    , DateTime.Now, "".PadRight(10,'*') + request.Id.Substring(10)
                    , nameof(RegisterOwnerCommand));


                return response;
            }
        }
    }
}