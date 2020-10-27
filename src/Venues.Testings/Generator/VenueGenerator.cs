using PingDong.Testings;

namespace PingDong.Newmoon.Venues.Tests.Generators
{
    internal class VenueGenerator : ModelGenerator
    {
        public static Venue Create()
        {
            var venue = new Venue("Venue" + NextInt(1, 9), AddressGenerator.Create());
            venue.AppendTraceMetadata("tenantId", "correlationId");

            return venue;
        }
    }
}
