using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PingDong.CleanArchitect.Service;

namespace PingDong.Newmoon.Places.Core
{
    public class Registrar
    {
        public virtual void Register(IServiceCollection services)
        {
            // MediatR: Register all behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            // FluentValidation: Register all validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
