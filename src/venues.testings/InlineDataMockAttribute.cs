using AutoFixture.Xunit2;

namespace PingDong.Newmoon.Venues.Tests
{
    public class InlineDataAutoMockAttribute : InlineAutoDataAttribute
    {
        public InlineDataAutoMockAttribute(params object[] values)
            : base(new MockInjectionAttribute(), values)
        { }
    }
}
