using System.Collections.Generic;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Core
{
    // Dev Note:
    //    Two important characteristics for value objects:
    //      Without Identity
    //      Immutable    

    public class Address : ValueObject
    {
        public Address(string no, string street, string city, string state, string country, string zipCode)
        {
            No = no;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public string No { get; }
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return No;
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
