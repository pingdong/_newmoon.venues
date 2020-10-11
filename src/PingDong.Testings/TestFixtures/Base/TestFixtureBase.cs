using System;

namespace PingDong.Testings.TestFixtures
{
    public class TestFixtureBase : IDisposable
    {
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                DisposeManagedResource();

            _disposed = true;
        }

        protected virtual void DisposeManagedResource()
        {

        }
    }
}
