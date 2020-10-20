using MediatR;
using PingDong.DDD.Infrastructure;
using PingDong.DDD.Services;
using PingDong.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueOccupyCommandHandler : CommandHandler, IRequestHandler<VenueOccupyCommand, bool>
    {
        private readonly IRepository<Guid, Venue> _repository;

        public VenueOccupyCommandHandler(
            IRepository<Guid, Venue> repository)
            : base(repository)
        {
            _repository = repository.EnsureNotNull(nameof(repository));
        }

        public async Task<bool> Handle(VenueOccupyCommand command, CancellationToken cancellationToken)
        {
            var venue = await GetVenueAsync(command.Id, command);

            venue.Occupy();

            await _repository.UpdateAsync(venue);

            return await _repository.UnitOfWork.SaveAsync(cancellationToken);
        }
    }

    public class VenueOccupyIdentifiedCommandHandler : IdentifiedCommandHandler<bool, VenueOccupyCommand>
    {
        public VenueOccupyIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) 
            : base(mediator, requestManager, true)
        {
        }
    }
}
