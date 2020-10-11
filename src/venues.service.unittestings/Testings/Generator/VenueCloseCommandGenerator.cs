using PingDong.Newmoon.Venues.Services.Commands;
using PingDong.Testings;

namespace PingDong.Newmoon.Venues.Testings
{
    internal class VenueCloseCommandGenerator : ModelGenerator
    {
        public static VenueCloseCommand Create()
        {
            var cmd = new VenueCloseCommand();
            cmd.Id = NextGuid();
            cmd.AppendTraceMetadata("tenantId", "correlationId");

            return cmd;
        }
    }
}
