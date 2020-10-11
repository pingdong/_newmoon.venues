using FluentValidation;
using System;

namespace PingDong.Azure.Function
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IServiceProvider _services;

        public ValidatorFactory(IServiceProvider services)
        {
            _services = services.EnsureNotNull(nameof(services));
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _services.GetService(validatorType) as IValidator;
        }
    }
}
