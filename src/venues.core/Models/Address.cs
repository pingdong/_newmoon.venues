using PingDong.DDD;
using System.Collections.Generic;

namespace PingDong.Newmoon.Venues
{
    // Dev Note:
    //    Two important characteristics for value objects:
    //      Without Identity
    //      Immutable    

    public class Address : ValueObject
    {
        public Address()
        {
        }

        public Address(string no, string street, string city, string state, string country, string zipCode)
        {
            No = no;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public string No { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        
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
