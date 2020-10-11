using System.Collections.Generic;
using System.Linq;

namespace PingDong.Http
{
    public class SuccessResult : HttpResult
    {
        public SuccessResult(object value)
            : base(true)
        {
            Value = value.EnsureNotNull(nameof(value));

            Count = value is IEnumerable<object> list
                        ? list.Count()
                        : 1;
        }

        public object Value { get; }

        public int Count { get; }
    }
}
