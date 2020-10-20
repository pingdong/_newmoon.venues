using MediatR;
using PingDong.DDD.Infrastructure;
using PingDong.DDD.Services;
using PingDong.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueOpenCommandHandler : CommandHandler, IRequestHandler<VenueOpenCommand, bool>
    {
        private readonly IRepository<Guid, Venue> _repository;

        public VenueOpenCommandHandler(
            IRepository<Guid, Venue> repository)
            : base(repository)
        {
            _repository = repository.EnsureNotNull(nameof(repository));
        }

        public async Task<bool> Handle(VenueOpenCommand command, CancellationToken cancellationToken)
        {
            var venue = await GetVenueAsync(command.Id, command);

            venue.Open();

            await _repository.UpdateAsync(venue);

            return await _repository.UnitOfWork.SaveAsync(cancellationToken);
        }
    }

    public class VenueOpenIdentifiedCommandHandler : IdentifiedCommandHandler<bool, VenueOpenCommand>
    {
        public VenueOpenIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) 
            : base(mediator, requestManager, true)
        {
        }
    }
}
