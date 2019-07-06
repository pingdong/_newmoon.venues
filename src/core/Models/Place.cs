using System;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Core
{
    public class Place : Entity<Guid>, IAggregateRoot
    {
        #region ctor

        public Place(string name) : this(name, null)
        {

        }

        public Place(string name, Address address)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            Address = address != null && address.IsValid ? address : throw new ArgumentNullException(nameof(address));
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        public Address Address { get; private set; }

        public void Update(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        #endregion

        #region Occupied

        public bool IsOccupied { get; private set; }

        public void Engage()
        {
            if (IsOccupied)
                throw new DomainException($"{Name} is occupied");

            IsOccupied = true;

            AddDomainEvent(new PlaceStateChangedDomainEvent(Id, Name, IsOccupied));
        }

        public void Disengage()
        {
            if (!IsOccupied)
                throw new DomainException($"{Name} is unoccupied");

            IsOccupied = false;

            AddDomainEvent(new PlaceStateChangedDomainEvent(Id, Name, IsOccupied));
        }

        #endregion
    }
}
