using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PingDong.Validations
{
    [Serializable]
    public class ValidationFailureException : DomainException
    {
        public ValidationFailureException()
        {
        }

        public ValidationFailureException(string message)
            : this(message, null, null)
        {
        }

        public ValidationFailureException(IEnumerable<ValidationError> errors)
            : this(null, errors, null)
        {
        }


        public ValidationFailureException(string message, IEnumerable<ValidationError> errors)
            : this(message, errors, null)
        {

        }

        public ValidationFailureException(string message, IEnumerable<ValidationError> errors, Exception inner)
            : base(message, inner)
        {
            Errors = errors.EnsureNotNull(nameof(errors));
        }

        protected ValidationFailureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public IEnumerable<ValidationError> Errors { get; }
    }
}
