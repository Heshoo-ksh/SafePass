namespace SafePass.Services
{
    public interface IMediatorService
    {
        void Notify(string eventName, object data = null);
        void Register(string eventName, Action<object> callback);
    }
}
