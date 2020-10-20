using System;
using Xunit;

namespace PingDong.Newmoon.Venues.Services
{
    public class RequestManagerTest
    {
        [Fact]
        public void CreateAsync_ShouldThrowException_IfTenantIdIsNull()
        {
            var mgr = new RequestManager();

            // ACT
            Assert.ThrowsAsync<ArgumentNullException>(() => mgr.CreateAsync(null, true));
            Assert.ThrowsAsync<ArgumentNullException>(() => mgr.CreateAsync(null, false));
        }

        [Fact]
        public void CreateAsync_ShouldThrowException_IfTenantIdIsEmpty()
        {
            var mgr = new RequestManager();

            // ACT
            Assert.ThrowsAsync<ArgumentNullException>(() => mgr.CreateAsync(string.Empty, true));
            Assert.ThrowsAsync<ArgumentNullException>(() => mgr.CreateAsync(string.Empty, false));
        }

        [Fact]
        public void CreateAsync_ShouldThrowException_IfTenantIdIsWhitespace()
        {
            var mgr = new RequestManager();

            // ACT
            Assert.ThrowsAsync<ArgumentNullException>(() => mgr.CreateAsync(" ", true));
            Assert.ThrowsAsync<ArgumentNullException>(() => mgr.CreateAsync(" ", false));
        }
    }
}
