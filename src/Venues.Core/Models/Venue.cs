using PingDong.DDD;
using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Newmoon.Venues.Validations;
using System;
using PingDong.Validations;

namespace PingDong.Newmoon.Venues
{
    public class Venue : Entity<Guid>, IAggregateRoot
    {
        #region ctor

        // TODO: Full support of ValueObject
        // Under the limit support of ValueObject in EF Core 2.x
        // Have to provide a ctor that can ignore ValueObject
        
        public Venue(string name, Address address = null)
        {
            Validate(name, address);

            Name = name;
            Address = address;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        public Address Address { get; private set; }

        public VenueStatus Status => VenueStatus.From(_venueStatusId);

        #endregion

        #region Methods

        public void Update(string name, Address address)
        {
            Validate(name, address);

            Name = name;
            Address = address;
        }

        private void Validate(string name, Address address)
        {
            name.EnsureNotNullOrWhitespace(nameof(name));

            address?.EnsureValidated(new AddressValidator());
        }

        #endregion

        #region Status

        private int _venueStatusId = VenueStatus.Unavailable.Id;

        public void Occupy()
        {
            if (_venueStatusId == VenueStatus.Occupied.Id)
                throw new EntityException($"{Name} is already occupied", this);
            if (_venueStatusId == VenueStatus.Unavailable.Id)
                throw new EntityException($"{Name} is unavailable", this);

            _venueStatusId = VenueStatus.Occupied.Id;
            
            AddDomainEvent(new VenueOccupiedDomainEvent(Id));
        }

        public void Vacate()
        {
            if (_venueStatusId == VenueStatus.Vacancy.Id)
                throw new EntityException($"{Name} is vacancy. Unable to vacate", this);
            if (_venueStatusId == VenueStatus.Unavailable.Id)
                throw new EntityException($"{Name} is unavailable.", this);

            _venueStatusId = VenueStatus.Vacancy.Id;
            
            AddDomainEvent(new VenueVacatedDomainEvent(Id));
        }

        public void Close()
        {
            if (_venueStatusId == VenueStatus.Unavailable.Id)
                throw new EntityException($"{Name} is unavailable", this);
            if (_venueStatusId == VenueStatus.Occupied.Id)
                throw new EntityException($"{Name} is occupied, unable to close an occupied venue", this);

            _venueStatusId = VenueStatus.Unavailable.Id;

            AddDomainEvent(new VenueClosedDomainEvent(Id));
        }

        public void Open()
        {
            if (_venueStatusId == VenueStatus.Vacancy.Id)
                throw new EntityException($"{Name} is already opened", this);
            if (_venueStatusId == VenueStatus.Occupied.Id)
                throw new EntityException($"{Name} is already occupied", this);

            _venueStatusId = VenueStatus.Vacancy.Id;
            
            AddDomainEvent(new VenueOpenedDomainEvent(Id));
        }

        #endregion
    }
}
