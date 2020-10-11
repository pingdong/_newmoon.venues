using MediatR;
using PingDong.CQRS.Infrastructure;
using PingDong.CQRS.Services;
using PingDong.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueVacateCommandHandler : CommandHandler, IRequestHandler<VenueVacateCommand, bool>
    {
        private readonly IRepository<Guid, Venue> _repository;

        public VenueVacateCommandHandler(
            IRepository<Guid, Venue> repository)
            : base(repository)
        {
            _repository = repository.EnsureNotNull(nameof(repository));
        }

        public async Task<bool> Handle(VenueVacateCommand command, CancellationToken cancellationToken)
        {
            var venue = await GetVenueAsync(command.Id, command);

            venue.Vacate();

            await _repository.UpdateAsync(venue);

            return await _repository.UnitOfWork.SaveAsync(cancellationToken);
        }
    }

    public class VenueVacateIdentifiedCommandHandler : IdentifiedCommandHandler<bool, VenueVacateCommand>
    {
        public VenueVacateIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) 
            : base(mediator, requestManager, true)
        {
        }
    }
}