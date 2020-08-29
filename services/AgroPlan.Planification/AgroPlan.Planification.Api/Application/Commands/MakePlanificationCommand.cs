using core = AgroPlan.Planification.Core.Aggregate;
using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Planification.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using AgroPlan.Planification.Core.ValueObjects;

namespace AgroPlan.Planification.Api.Application.Commands
{
    public class MakePlanificationCommand : IRequest<bool>
    {
        public MakePlanificationCommand(string clientId)
        {
            ClientId = clientId;
        }

        public string ClientId { get; set; }

        internal class MakePlanificationHandler : IRequestHandler<MakePlanificationCommand, bool>
        {
            private readonly IPlanificationRepository _repo;
            private readonly IRepository<core.Client, string> _cRepo;
            private readonly ILogger _log;

            public MakePlanificationHandler(
                IPlanificationRepository repo
                , IRepository<core.Client, string> cRepo
                , ILogger<MakePlanificationHandler> log)
            {
                _repo = repo ?? throw new ArgumentNullException(nameof(repo));
                _cRepo = cRepo ?? throw new ArgumentNullException(nameof(cRepo));
                _log = log ?? throw new ArgumentNullException(nameof(log));
            }

            public async Task<bool> Handle(MakePlanificationCommand request, CancellationToken cancellationToken)
            {

                var client = await _cRepo.GetById(request.ClientId);

                _ = client ?? throw new NullReferenceException("Don't exist a client with this id!");

                bool planExist = await _repo.Exist(request.ClientId, DateTime.Now.Year);

                if (planExist) throw new ApplicationException("Already exist a planification for this client and year!");

                var planification = core.Planification.Create(
                    client
                    , DateTime.Now.Year);

                _log.LogInformation("-> Creating Planification - Planification { @Planification }", planification);

                await _repo.Update(planification);

                return await _repo.Uow.SaveChangesEventsAsync(cancellationToken);
            }
        }
    }
}