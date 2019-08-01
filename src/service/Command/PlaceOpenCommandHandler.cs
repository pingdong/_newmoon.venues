using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceOpenCommandHandler : CommandHandler, IRequestHandler<PlaceOpenCommand, bool>
    {
        private readonly IRepository<Guid, Place> _repository;

        public PlaceOpenCommandHandler(IRepository<Guid, Place> repository)
            : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(PlaceOpenCommand command, CancellationToken cancellationToken)
        {
            var place = await GetPlaceAndEnsurePlaceNotExistedAsync(command.Id, command);
            
            place.Open();

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
            // Ignore duplicate requests
            return true;
        }
    }
}
