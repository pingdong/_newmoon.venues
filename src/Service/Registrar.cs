using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PingDong.CleanArchitect.Service;

namespace PingDong.Newmoon.Places.Services
{
    public class Registrar
    {
        public virtual void Register(IServiceCollection services)
        {
            // Idempotent: Register RequestManager
            services.AddScoped(typeof(IRequestManager<>), typeof(RequestManager<>));

            // MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
