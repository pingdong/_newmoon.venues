using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace PingDong.Newmoon.Places.Core
{
    public class Registrar
    {
        public virtual void Register(IServiceCollection services)
        {
            // Register all validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
