using FluentValidation;
using MediatR.Pipeline;
using PingDong.Validations;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.DDD.Validations
{
    public class ValidatorPreprocessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IValidatorFactory _validatorFactory;

        public ValidatorPreprocessor(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory.EnsureNotNull(nameof(validatorFactory));
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var validator = _validatorFactory.GetValidator<TRequest>();
            if (validator == null)
                return;

            await request.EnsureValidatedAsync(validator).ConfigureAwait(false);
        }
    }
}
