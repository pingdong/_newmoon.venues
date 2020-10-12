using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues
{
    public interface IVenueQueryService
    {
        Task<IEnumerable<Venue>> GetAllAsync();
        Task<Venue> GetByIdAsync(Guid venueId);
    }
}
