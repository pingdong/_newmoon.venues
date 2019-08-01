using System;
using PingDong.CleanArchitect.Core;
using PingDong.Newmoon.Places.Core.Validations;

namespace PingDong.Newmoon.Places.Core
{
    public class Place : Entity<Guid>, IAggregateRoot
    {
        #region ctor

        // [TODO] Full support of ValueObject
        // Under the limit support of ValueObject in EF Core 2.x
        // Have to provide a ctor that can ignore ValueObject
        public Place(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }

        public Place(string name, Address address)
        {
            Validate(name, address);

            Name = name;
            Address = address;
        }

        #endregion

        #region General

        public string Name { get; private set; }

        public Address Address { get; private set; }

        public void Update(string name, Address address)
        {
            Validate(name, address);

            Name = name;
            Address = address;
        }

        private void Validate(string name, Address address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            var addressValidator = new AddressValidator();
            var result = addressValidator.Validate(address);
            if (!result.IsValid)
                throw new ArgumentException(nameof(address));
        }

        #endregion

        #region State

        private int _placeStateId = PlaceState.Free.Id;
        public PlaceState State => PlaceState.From(_placeStateId);

        public void Occupy()
        {
            if (_placeStateId == PlaceState.Occupied.Id)
                throw new EntityException(EventIds.ViolateBusinessLogic, $"{Name} is already occupied", this);
            if (_placeStateId == PlaceState.TemporaryClosed.Id)
                throw new EntityException(EventIds.ViolateBusinessLogic, $"{Name} is temporary closed, unable to occupy", this);

            _placeStateId = PlaceState.Occupied.Id;
            
            AddDomainEvent(new PlaceOccupiedDomainEvent(Id, Name));
        }

        public void Free()
        {
            if (_placeStateId == PlaceState.Free.Id)
                throw new EntityException(EventIds.ViolateBusinessLogic, $"{Name} is free. Unable to free", this);
            if (_placeStateId == PlaceState.TemporaryClosed.Id)
                throw new EntityException(EventIds.ViolateBusinessLogic, $"{Name} is temporary closed. Unable to free", this);

            _placeStateId = PlaceState.Free.Id;
            
            AddDomainEvent(new PlaceFreedDomainEvent(Id, Name));
        }

        public void Close()
        {
            if (_placeStateId == PlaceState.TemporaryClosed.Id)
                throw new EntityException(EventIds.ViolateBusinessLogic, $"{Name} is already closed", this);
            if (_placeStateId == PlaceState.Occupied.Id)
                throw new EntityException(EventIds.ViolateBusinessLogic, $"{Name} is occupied, unable to close an occupied place", this);

            _placeStateId = PlaceState.TemporaryClosed.Id;

            AddDomainEvent(new PlaceClosedDomainEvent(Id, Name));
        }

        public void Open()
        {
            if (_placeStateId == PlaceState.Free.Id)
                throw new EntityException(EventIds.ViolateBusinessLogic, $"{Name} is already opened", this);
            if (_placeStateId == PlaceState.Occupied.Id)
                throw new EntityException(EventIds.ViolateBusinessLogic, $"{Name} is already occupied. Unable to open it", this);

            _placeStateId = PlaceState.Free.Id;
            
            AddDomainEvent(new PlaceFreedDomainEvent(Id, Name));
        }

        #endregion
    }
}
