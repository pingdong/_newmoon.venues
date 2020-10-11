using System.Threading.Tasks;

namespace PingDong.Services
{
    public interface IProcessor<in T>
    {
        Task ProcessAsync(T message);
    }
}
