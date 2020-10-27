using PingDong.Testings.TestFixtures;

namespace PingDong.Newmoon.Venues.Testings.TestFixtures
{
    public class TestSettingsTestFixture : TestFixtureBase
    {
        public TestSettingsTestFixture()
        {
            Settings = SettingsHelper.Build();
        }

        public TestSettings Settings { get; }
    }
}
