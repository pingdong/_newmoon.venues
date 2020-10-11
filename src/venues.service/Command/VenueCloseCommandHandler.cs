using System;
using MediatR;
using PingDong.CQRS.Infrastructure;
using PingDong.CQRS.Services;
using PingDong.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueCloseCommandHandler : CommandHandler, IRequestHandler<VenueCloseCommand, bool>
    {
        private readonly IRepository<Guid, Venue> _repository;

        public VenueCloseCommandHandler(
            IRepository<Guid, Venue> repository)
            : base(repository)
        {
            _repository = repository.EnsureNotNull(nameof(repository));
        }

        public async Task<bool> Handle(VenueCloseCommand command, CancellationToken cancellationToken = default)
        {
            var venue = await GetVenueAsync(command.Id, command);

            venue.Close();

            await _repository.UpdateAsync(venue);

            return await _repository.UnitOfWork.SaveAsync(cancellationToken);
        }
    }

    public class VenueCloseIdentifiedCommandHandler : IdentifiedCommandHandler<bool, VenueCloseCommand>
    {
        public VenueCloseIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) 
            : base(mediator, requestManager, true)
        {
        }
    }
}