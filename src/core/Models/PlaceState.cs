using System;
using System.Collections.Generic;
using System.Linq;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceState : Enumeration
    {
        
        public static PlaceState Free = new PlaceState(1, nameof(Free).ToLowerInvariant());
        public static PlaceState Occupied = new PlaceState(2, nameof(Occupied).ToLowerInvariant());
        public static PlaceState TemporaryClosed = new PlaceState(3, nameof(TemporaryClosed).ToLowerInvariant());
        
        public PlaceState(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<PlaceState> List() => new[] { Free, Occupied, TemporaryClosed };

        public static PlaceState From(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state is null)
                throw new DomainException($"Possible values for PlaceState: { string.Join(",", List().Select(s => s.Name)) }");

            return state;
        }

        public static PlaceState From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state is null)
                throw new DomainException($"Possible values for PlaceState: { string.Join(",", List().Select(s => s.Name)) }");

            return state;
        }
    }
}
