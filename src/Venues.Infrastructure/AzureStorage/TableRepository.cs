using PingDong.Newmoon.Venues.Models;
using System;
using System.Collections.Generic;

namespace PingDong.Newmoon.Venues.Infrastructure
{
    public class AzureTableStorageRepository : IRepository<Guid, Venue>
    {
        private readonly ICache<Guid, Venue> _cache;

        public AzureTableStorageRepository(ICache<Guid, Venue> cache)
        {
            _cache = cache.EnsureNotNull(nameof(cache));
        }

        public IList<Venue> GetAll()
        {
            throw new NotImplementedException();
        }

        public Venue GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void CreateOrUpdate(Venue venue)
        {
            throw new NotImplementedException();
        }
    }
}