using PingDong.Validations;
using System.Collections.Generic;

namespace PingDong.Http
{
    public class FailedResult : HttpResult
    {
        public FailedResult(string message)
            : this(message, null)
        {
            Message = message;
        }
        public FailedResult(string message, IEnumerable<ValidationError> validationErrors)
            : base(false)
        {
            Message = message.EnsureNotNullOrWhitespace(nameof(message));
            ValidationErrors = validationErrors.EnsureNotNull(nameof(validationErrors));
        }

        public string Message { get; }

        public IEnumerable<ValidationError> ValidationErrors { get; }
    }
}
