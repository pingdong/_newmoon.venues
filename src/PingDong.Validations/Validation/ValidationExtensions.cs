using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PingDong.Validations
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Validate the given object
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="target">The target object</param>
        /// <param name="validator">An IValidator of the type of the target</param>
        /// <exception cref="ValidationFailureException">If the object is incorrect, an exception will be thrown</exception>
        /// <exception cref="ArgumentNullException">If the validator is null</exception>
        public static T EnsureValidated<T>(this T target, IValidator<T> validator)
        {
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            var result = validator.Validate(target);

            if (!result.IsValid)
                throw new ValidationFailureException(ToErrors(result.Errors));

            return target;
        }

        /// <summary>
        /// Validate the given object
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="target">The target object</param>
        /// <param name="validator">An IValidator of the type of the target</param>
        /// <exception cref="ValidationFailureException">If the object is incorrect, an exception will be thrown</exception>
        /// <exception cref="ArgumentNullException">If the validator is null</exception>
        public static async Task<T> EnsureValidatedAsync<T>(this T target, IValidator<T> validator)
        {
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            var result = await validator.ValidateAsync(target).ConfigureAwait(false);

            if (!result.IsValid)
                throw new ValidationFailureException(ToErrors(result.Errors));

            return target;
        }

        #region Private Methods

        public static IEnumerable<ValidationError> ToErrors(this IList<ValidationFailure> errors)
        {
            return errors.Select(error => new ValidationError
            {
                ErrorCode = error.ErrorCode,
                ErrorMessage = error.ErrorMessage,
                PropertyName = error.PropertyName
            });
        }

        #endregion
    }
}
