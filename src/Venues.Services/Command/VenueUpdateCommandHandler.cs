using MediatR;
using PingDong.DDD.Infrastructure;
using PingDong.DDD.Services;
using PingDong.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueUpdateCommandHandler : CommandHandler, IRequestHandler<VenueUpdateCommand, bool>
    {
        private readonly IRepository<Guid, Venue> _repository;

        public VenueUpdateCommandHandler(
            IRepository<Guid, Venue> repository)
            : base(repository)
        {
            _repository = repository.EnsureNotNull(nameof(repository));
        }

        public async Task<bool> Handle(VenueUpdateCommand command, CancellationToken cancellationToken)
        {
            var venue = await GetVenueAsync(command.Id, command);

            venue.Update(command.Name, command.Address);

            await _repository.UpdateAsync(venue);

            return await _repository.UnitOfWork.SaveAsync(cancellationToken);
        }
    }

    public class VenueUpdateIdentifiedCommandHandler : IdentifiedCommandHandler<bool, VenueUpdateCommand>
    {
        public VenueUpdateIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager)
            : base(mediator, requestManager, true)
        {
        }
    }
}
