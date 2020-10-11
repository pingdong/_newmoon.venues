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

        public async Task<Venue> GetVenueAsync(Guid placeId, IRequestMetadata metadata, bool throwExceptionIfNotExisted = true)
        {
            var venue = await _repository.FindByIdAsync(placeId, throwExceptionIfNotExisted);
            if (venue == null)
            {
                return throwExceptionIfNotExisted
                    ? throw new NotFoundException("place", placeId.ToString(), metadata.CorrelationId, metadata.TenantId)
                    : (Venue) null;
            }

            venue.AppendTraceMetadata(metadata);

            return venue;
        }

        public Venue CreateVenue(string venueName, Address address, IRequestMetadata metadata)
        {
            var venue = new Venue(venueName, address);
            venue.AppendTraceMetadata(metadata);

            return venue;
        }
    }
}
