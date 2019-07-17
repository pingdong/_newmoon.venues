using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceOpenCommandHandler : IRequestHandler<PlaceOpenCommand, bool>
    {
        private readonly IRepository<Guid, Place> _repository;

        public PlaceOpenCommandHandler(IRepository<Guid, Place> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(PlaceOpenCommand command, CancellationToken cancellationToken)
        {
            var place = await _repository.FindByIdAsync(command.Id);
            if (place == null)
                return false;
            
            place.Prepare(command).Open();

            await _repository.UpdateAsync(place);
            
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class PlaceOpenIdentifiedCommandHandler : IdentifiedCommandHandler<Guid, bool, PlaceOpenCommand>
    {
        public PlaceOpenIdentifiedCommandHandler(IMediator mediator, IRequestManager<Guid> requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            // Ignore duplicate requests for creating order.
            return true;
        }
    }
}
