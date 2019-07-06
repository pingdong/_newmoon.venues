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
    public class PlaceEngageCommandHandler : IRequestHandler<PlaceEngageCommand, bool>
    {
        private readonly IRepository<Guid, Place> _repository;

        public PlaceEngageCommandHandler(IRepository<Guid, Place> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(PlaceEngageCommand message, CancellationToken cancellationToken)
        {
            var place = await _repository.FindByIdAsync(message.Id);
            if (place == null)
                return false;

            place.Engage();
            
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class PlaceEngageIdentifiedCommandHandler : IdentifiedCommandHandler<Guid, bool, PlaceEngageCommand>
    {
        public PlaceEngageIdentifiedCommandHandler(IMediator mediator, IRequestManager<Guid> requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            // Ignore duplicate requests for creating order.
            return true;
        }
    }
}
