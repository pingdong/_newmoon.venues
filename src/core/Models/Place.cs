using System;
using PingDong.CleanArchitect.Core;

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
            Name = string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }

        public Place(string name, Address address)
        {
            Validate(name, address);

            Name = name;
            Address = address;
        }

        #endregion

        #region Properties

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
        }

        #endregion

        #region Occupied

        public bool IsOccupied { get; private set; }

        public void Engage()
        {
            if (IsOccupied)
                throw new DomainException($"{Name} is occupied");

            IsOccupied = true;

            AddDomainEvent(new PlaceEngagedDomainEvent(Id, Name));
        }

        public void Disengage()
        {
            if (!IsOccupied)
                throw new DomainException($"{Name} is unoccupied");

            IsOccupied = false;

            AddDomainEvent(new PlaceDisengagedDomainEvent(Id, Name));
        }

        #endregion
    }
}
