namespace PingDong.Http
{
    public abstract class HttpResult
    {
        protected HttpResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}
