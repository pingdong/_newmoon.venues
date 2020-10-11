namespace PingDong
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}
