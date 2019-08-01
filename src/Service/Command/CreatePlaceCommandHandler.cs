using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class CreatePlaceCommandHandler : CommandHandler, IRequestHandler<CreatePlaceCommand, bool>
    {
        private readonly IRepository<Guid, Place> _repository;

        public CreatePlaceCommandHandler(IRepository<Guid, Place> repository)
            : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(CreatePlaceCommand command, CancellationToken cancellationToken)
        {
            var place = CreatePlace(command, command.Name, command.Address);

            await _repository.AddAsync(place);

            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class CreatePlaceIdentifiedCommandHandler : IdentifiedCommandHandler<Guid, bool, CreatePlaceCommand>
    {
        public CreatePlaceIdentifiedCommandHandler(IMediator mediator, IRequestManager<Guid> requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            // Ignore duplicate requests
            return true;
        }
    }
}
