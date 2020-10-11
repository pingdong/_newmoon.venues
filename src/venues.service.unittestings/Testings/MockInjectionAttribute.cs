using AutoFixture;
using AutoFixture.Xunit2;
using PingDong.Newmoon.Venues.Testings.Generator;

namespace PingDong.Newmoon.Venues.Testings
{
    public class ServiceMockInjectionAttribute : AutoDataAttribute
    {
        public ServiceMockInjectionAttribute() : base(Build)
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
