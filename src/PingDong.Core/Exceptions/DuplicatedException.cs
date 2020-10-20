using System;
using System.Runtime.Serialization;

namespace PingDong
{
    [Serializable]
    public class DuplicatedException : DomainException
    {
        public DuplicatedException()
            : base("Duplicated data exists")
        {
        }

        public DuplicatedException(string message)
            : base(message)
        {
        }

        public DuplicatedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DuplicatedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
