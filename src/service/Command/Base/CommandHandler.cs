using System;
using System.Threading.Tasks;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.Newmoon.Places.Core;
using PingDong.Newmoon.Places.Service.Commands;

namespace PingDong.Newmoon.Places.Service
{
    public class CommandHandler
    {
        private readonly IRepository<Guid, Place> _repository;

        public CommandHandler(IRepository<Guid, Place> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Place> GetPlaceAndEnsurePlaceNotExistedAsync(Guid placeId, ITracker tracker)
        {
            var place = await _repository.FindByIdAsync(placeId);
            if (place == null)
                throw new ObjectNotFoundException("place", placeId.ToString(), tracker);

            return place.Preprocess(tracker);
        }

        public Place CreatePlace(ITracker tracker, string name, Address address)
        {
            var place = new Place(name, address);

            return place.Preprocess(tracker);
        }
    }
}
