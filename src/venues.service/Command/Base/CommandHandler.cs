using PingDong.CQRS.Infrastructure;
using System;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class CommandHandler
    {
        private readonly IRepository<Guid, Venue> _repository;

        public CommandHandler(IRepository<Guid, Venue> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Venue> GetVenueAsync(Guid venueId, IRequestMetadata metadata, bool throwExceptionIfNotExisted = true)
        {
            venueId.EnsureNotNullOrDefault(nameof(venueId));
            metadata.EnsureNotNull(nameof(metadata));

            var venue = await _repository.FindByIdAsync(venueId, throwExceptionIfNotExisted);

            if (venue == null)
                return null;

            venue.AppendTraceMetadata(metadata);
            return venue;
        }

        public Venue CreateVenue(string venueName, Address address, IRequestMetadata metadata)
        {
            venueName.EnsureNotNullDefaultOrWhitespace(nameof(venueName));
            address.EnsureNotNull(nameof(address));
            metadata.EnsureNotNull(nameof(metadata));

            var venue = new Venue(venueName, address);
            venue.AppendTraceMetadata(metadata);

            return venue;
        }
    }
}
