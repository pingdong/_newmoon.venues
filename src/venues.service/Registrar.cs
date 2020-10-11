using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PingDong.CQRS.Infrastructure;
using PingDong.CQRS.Services;
using PingDong.Messages;
using PingDong.Newmoon.Venues.Infrastructure;
using PingDong.Services;
using System;
using System.Reflection;

namespace PingDong.Newmoon.Venues.Services
{
    public class Registrar
    {
        public virtual void Register(IServiceCollection services)
        {
            services.AddScoped<IRequestManager, RequestManager>();
            services.AddScoped(typeof(ITenantManager<string>), typeof(TenantManager));

            // MediatR
            services.AddMediatR(
                typeof(DomainEventHandler).Assembly,
                Assembly.GetExecutingAssembly()
            );

            // FluentValidation
            services.AddValidatorsFromAssemblies(new [] {
                //    From this assembly
                Assembly.GetExecutingAssembly()
            });


            // Dump
            services.AddSingleton<IRepository<Guid, Venue>, DumpRepository>();
            services.AddSingleton<DumpContext>();
            services.AddSingleton<IPublisher, DumpPublisher>();
        }
    }
}
