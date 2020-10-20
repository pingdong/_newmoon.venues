using PingDong.Newmoon.Venues.Models;
using System;
using System.Collections.Generic;

namespace PingDong.Newmoon.Venues.Infrastructure
{
    public class AzureBlobStorageRepository : IRepository
    {
        private readonly ICache _cache;

        public AzureBlobStorageRepository(ICache cache)
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

        public void Update(Venue venue)
        {
            throw new NotImplementedException();
        }
    }
}
