using PingDong.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PingDong.Newmoon.Venues
{
    public class VenueStatus : Enumeration
    {
        public static VenueStatus Vacancy = new VenueStatus(1, nameof(Vacancy).ToLowerInvariant());
        public static VenueStatus Occupied = new VenueStatus(2, nameof(Occupied).ToLowerInvariant());
        public static VenueStatus Available = new VenueStatus(3, nameof(Available).ToLowerInvariant());
        public static VenueStatus Unavailable = new VenueStatus(4, nameof(Unavailable).ToLowerInvariant());

        public VenueStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<VenueStatus> List() => new[] { Vacancy, Occupied, Available, Unavailable };

        public static VenueStatus From(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state is null)
                throw new EntityException($"Possible values for VenueState: { string.Join(",", List().Select(s => s.Name)) }");

            return state;
        }

        public static VenueStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state is null)
                throw new EntityException($"Possible values for VenueState: { string.Join(",", List().Select(s => s.Name)) }");

            return state;
        }
    }
}
