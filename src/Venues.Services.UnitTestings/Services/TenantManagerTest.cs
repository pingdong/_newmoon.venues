using System;
using Xunit;

namespace PingDong.Newmoon.Venues.Services
{
    public class TenantManagerTest
    {
        [Fact]
        public void CreateAsync_ShouldThrowException_IfTenantIdIsNull()
        {
            var mgr = new TenantManager();

            // ACT
            Assert.ThrowsAsync<ArgumentNullException>(() => mgr.CreateAsync(null));
        }

        [Fact]
        public void CreateAsync_ShouldThrowException_IfTenantIdIsEmpty()
        {
            var mgr = new TenantManager();

            // ACT
            Assert.ThrowsAsync<ArgumentNullException>(() => mgr.CreateAsync(string.Empty));
        }

        [Fact]
        public void CreateAsync_ShouldThrowException_IfTenantIdIsWhitespace()
        {
            var mgr = new TenantManager();

            // ACT
            Assert.ThrowsAsync<ArgumentNullException>(() => mgr.CreateAsync(" "));
        }
    }
}
