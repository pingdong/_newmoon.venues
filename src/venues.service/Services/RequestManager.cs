using PingDong.Services;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services
{
    public class RequestManager : IRequestManager
    {
        public Task CreateAsync(string requestId, bool suppressDuplicatedError)
        {
            // TODO: Implement
            return Task.CompletedTask;
        }
    }
}
