using MediatR;
using PingDong.DDD.Infrastructure;
using PingDong.DDD.Services;
using PingDong.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueRegisterCommandHandler : CommandHandler, IRequestHandler<VenueRegisterCommand, bool>
    {
        private readonly IRepository<Guid, Venue> _repository;

        public VenueRegisterCommandHandler(
            IRepository<Guid, Venue> repository)
            : base(repository)
        {
            _repository = repository.EnsureNotNull(nameof(repository));
        }

        public async Task<bool> Handle(VenueRegisterCommand command, CancellationToken cancellationToken)
        {
            var venue = CreateVenue(command.Name, command.Address, command);

            await _repository.AddAsync(venue);

            return await _repository.UnitOfWork.SaveAsync(cancellationToken);
        }
    }

    public class VenueRegisterIdentifiedCommandHandler : IdentifiedCommandHandler<bool, VenueRegisterCommand>
    {
        public VenueRegisterIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager)
            : base(mediator, requestManager, true)
        {
        }
    }
}
