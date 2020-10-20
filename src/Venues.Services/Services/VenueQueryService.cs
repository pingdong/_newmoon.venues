using PingDong.DDD.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services
{
    public class VenueQueryService : IVenueQueryService
    {
        private readonly IRepository<Guid, Venue> _repository;

        public VenueQueryService(IRepository<Guid, Venue> repository)
        {
            _repository = repository.EnsureNotNull(nameof(repository));
        }


        public async Task<IEnumerable<Venue>> GetAllAsync()
        {
            return await _repository.ListAsync();
        }

        public async Task<Venue> GetByIdAsync(Guid venueId)
        {
            venueId.EnsureNotNullOrDefault(nameof(venueId));

            return await _repository.FindByIdAsync(venueId, false);
        }
    }
}
