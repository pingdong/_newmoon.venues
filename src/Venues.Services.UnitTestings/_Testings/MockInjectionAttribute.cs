using AutoFixture;
using AutoFixture.Xunit2;

namespace PingDong.Newmoon.Venues.Testings
{
    public class ServiceInjectionAttribute : AutoDataAttribute
    {
        public ServiceInjectionAttribute() : base(Build)
        {

        }

        public static IFixture Build()
        {
            var fixture = MockInjectionAttribute.Build();

            // Generator
            fixture.Register(VenueCloseCommandGenerator.Create);

            return fixture;
        }
    }
}
