using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.CleanArchitect.Service;
using PingDong.CleanArchitect.Service.Idempotency;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, bool>
    {
        private readonly IRepository<Guid, Place> _repository;

        public CreatePlaceCommandHandler(IRepository<Guid, Place> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(CreatePlaceCommand message, CancellationToken cancellationToken)
        {
            var place = new Place(message.Name, message.Address);

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
            // Ignore duplicate requests for creating order.
            return true;
        }
    }
}
