using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Core
{
    public class Registrar
    {
        public virtual void Register(IServiceCollection services)
        {
            // FluentValidation
            services.AddValidatorsFromAssemblies(new [] {
                //    From CleanArchitect.Core
                typeof(Entity<>).Assembly,
                //    From this assembly
                Assembly.GetExecutingAssembly()
            });
        }
    }
}
