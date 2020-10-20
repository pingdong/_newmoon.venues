using System;
using System.Runtime.Serialization;

namespace PingDong
{
    [Serializable]
    public class InvalidRequestException : DomainException
    {
        public InvalidRequestException()
            : base("The request was invalid")
        {
        }

        public InvalidRequestException(string message)
            : this(message, null)
        {
        }

        public InvalidRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
