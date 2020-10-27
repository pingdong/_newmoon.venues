using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PingDong.Newmoon.Venues
{
    namespace Core
    {
        public class Registrar
        {
            public void Register(IServiceCollection services)
            {
                // FluentValidation
                services.AddValidatorsFromAssemblies(new [] {
                    Assembly.GetExecutingAssembly()
                });
            }
        }
    }
}
