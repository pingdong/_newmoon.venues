using MediatR;
using PingDong.CQRS.Infrastructure;
using PingDong.CQRS.Services;
using PingDong.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueCreateCommandHandler : CommandHandler, IRequestHandler<VenueCreateCommand, bool>
    {
        private readonly IRepository<Guid, Venue> _repository;

        public VenueCreateCommandHandler(
            IRepository<Guid, Venue> repository)
            : base(repository)
        {
            _repository = repository.EnsureNotNull(nameof(repository));
        }

        public async Task<bool> Handle(VenueCreateCommand command, CancellationToken cancellationToken)
        {
            var venue = CreateVenue(command.Name, command.Address, command);

            await _repository.AddAsync(venue);

            return await _repository.UnitOfWork.SaveAsync(cancellationToken);
        }
    }

    public class VenueCreateIdentifiedCommandHandler : IdentifiedCommandHandler<bool, VenueCreateCommand>
    {
        public VenueCreateIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager)
            : base(mediator, requestManager, true)
        {
        }
    }
}
