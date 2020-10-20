using AutoFixture.Xunit2;

namespace PingDong.Newmoon.Venues.Testings
{
    public class InlineDataAutoMockAttribute : InlineAutoDataAttribute
    {
        public InlineDataAutoMockAttribute(params object[] values)
            : base(new MockInjectionAttribute(), values)
        { }
    }
}
