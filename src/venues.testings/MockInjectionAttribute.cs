using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Options;
using Moq;
using PingDong.Newmoon.Venues.Settings;
using PingDong.Newmoon.Venues.Testings.Generators;
using PingDong.Testings.Helper;

namespace PingDong.Newmoon.Venues.Testings
{
    public class MockInjectionAttribute : AutoDataAttribute
    {
        public MockInjectionAttribute() : base(Build)
        {

        }

        public static IFixture Build()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            // Mapping
            fixture.Register(() => MapperHelper.Build(typeof(AppSettings).Assembly));

            // Settings
            var setting = SettingsHelper.Build();
            var appOptionsMonitor = Mock.Of<IOptionsMonitor<AppSettings>>(s => s.CurrentValue == setting);
            fixture.Register(() => appOptionsMonitor);
            var testOptionsMonitor = Mock.Of<IOptionsMonitor<TestSettings>>(s => s.CurrentValue == setting);
            fixture.Register(() => testOptionsMonitor);

            // Resiliency
            //var cosmosResiliency = new Mock<ICosmosResiliencyHelper>();
            //cosmosResiliency.Setup(x => x.GetRetryPolicy())
            //    .Returns(Policy.NoOpAsync());
            //fixture.Register(() => cosmosResiliency);

            // Generator
            fixture.Register(AddressGenerator.Create);
            fixture.Register(VenueGenerator.Create);

            return fixture;
        }
    }
}
